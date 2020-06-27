using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Runner.Wpf;
using Xunit.Runner.Wpf.ViewModel;
using xUnitRevitUtils;

namespace xUnitRevit
{
  public static class TestRunner
  {
    public static void Launch(UIApplication uiapp)
    {
      try
      {
        var queue = new List<Action>();
        var eventHandler = ExternalEvent.Create(new ExternalEventHandler(queue));

        xru.Initialize(uiapp, SynchronizationContext.Current, eventHandler, queue);

        var main = new MainWindow();
        main.Title = "xUnit Revit by Speckle";
       /// (main.DataContext as MainViewModel).Injector =
        (main.DataContext as MainViewModel).StartupAssemblies = new List<string>() { @"C:\Code\Speckle-Next\SpeckleKitRevit\SpeckleRevitTests\bin\Debug\SpeckleRevitTests.dll" };
        main.Show();


      }
      catch (Exception e)
      {
      }

      //return Result.Succeeded;
    }
  }

  //public class Injector : IAssemblyInjector
  //{
  //  private UIApplication uiapp { get; set; }
  //  SynchronizationContext uiContext { get; set; }

  //  private List<Action> queue { get; set; }
  //  private ExternalEvent eventHandler { get; set; }

  //  public Injector(UIApplication uiapp, SynchronizationContext uiContext, ExternalEvent eventHandler, List<Action> queue)
  //  {
  //    this.uiapp = uiapp;
  //    this.uiContext = uiContext;
  //    this.eventHandler = eventHandler;
  //    this.queue = queue;
  //  }
  //  public void Inject(Assembly assembly)
  //  {
  //    var types = assembly.GetTypes();
  //    foreach (var type in types)
  //    {
  //      if (type.Name == "xru")
  //      {
  //        if (type.GetProperties().Select(p => p.Name).Contains("Uiapp"))
  //        {
  //          type.GetProperty("Uiapp").SetValue(null, uiapp);
  //          type.GetProperty("UiContext").SetValue(null, uiContext);
  //          type.GetProperty("Queue").SetValue(null, queue);
  //          type.GetProperty("EventHandler").SetValue(null, eventHandler);
  //        }
  //      }
  //    }
  //  }

  //}
}
