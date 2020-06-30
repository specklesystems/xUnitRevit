#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Xunit.Runner.Wpf;
using Xunit.Runner.Wpf.ViewModel;
#endregion

namespace xUnitRevit
{
  [Transaction(TransactionMode.Manual)]
  public class Command : IExternalCommand
  {
    static object consoleLock = new object();
    static ManualResetEvent finished = new ManualResetEvent(false);
    static Result result = Result.Succeeded;

    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      UIApplication uiapp = commandData.Application;
      Runner.Launch(uiapp);
      return Result.Succeeded;



    }





  }


}
