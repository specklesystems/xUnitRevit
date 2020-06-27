using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace xUnitRevitUtils
{
  public static class xru
  {
    private static UIApplication Uiapp { get; set; }

    private static List<Action> Queue { get; set; }
    private static ExternalEvent EventHandler { get; set; }

    private static SynchronizationContext UiContext { get; set; }

    public static Document ActiveDoc { get { return Uiapp.ActiveUIDocument.Document; } }
    public static Selection CuerrentSelection { get { return Uiapp.ActiveUIDocument.Selection; } }

    public static void Initialize(UIApplication uiapp, SynchronizationContext uiContext, ExternalEvent eventHandler, List<Action> queue)
    {
      Uiapp = uiapp;
      UiContext = uiContext;
      EventHandler = eventHandler;
      Queue = queue;
    }

    public static List<Element> GetActiveSelection()
    {
      if (Uiapp.ActiveUIDocument != null)
        return Uiapp.ActiveUIDocument.Selection.GetElementIds().Select(x => Uiapp.ActiveUIDocument.Document.GetElement(x)).ToList();
      return new List<Element>();
    }
    /// <summary>
    /// Opens and activates a document if not open already
    /// </summary>
    /// <param name="path"></param>
    public static Document OpenDoc(string path)
    {
      Assert.NotNull(Uiapp);
      Document doc = null;
      //OpenAndActivateDocument only works if run from the current context
      UiContext.Send(x => { doc = Uiapp.OpenAndActivateDocument(path).Document; }, null);
      Assert.NotNull(doc);
      return doc;
    }


    /// <summary>
    /// Creates a new empty document
    /// </summary>
    /// <param name="path"></param>
    public static Document CreateNewDoc(string templatePath, string filePath)
    {
      Assert.NotNull(Uiapp);
      Document doc = null;

      try
      {
        if (File.Exists(filePath))
          File.Delete(filePath);
      }
      catch { }

      //OpenAndActivateDocument only works if run from the current context
      UiContext.Send(x =>
      {
        if (!File.Exists(filePath))
        {
          doc = Uiapp.Application.NewProjectDocument(templatePath);
          doc.SaveAs(filePath);
          doc.Close();
        }

        doc = Uiapp.OpenAndActivateDocument(filePath).Document;
      }
      , null);
      Assert.NotNull(doc);
      return doc;
    }

    public static Task RunInTransaction(Action action, Document doc)
    {
      var tcs = new TaskCompletionSource<string>();
      Queue.Add(new Action(() =>
      {
        try
        {
          using (Transaction transaction = new Transaction(doc, "test transaction"))
          {
            transaction.Start();
            action.Invoke();
            transaction.Commit();

          }
        }
        catch (Exception e)
        {
          tcs.TrySetException(e);
        }
        tcs.TrySetResult("");
      }));

      EventHandler.Raise();

      return tcs.Task;
    }
  }
}
