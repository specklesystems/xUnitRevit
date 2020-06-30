#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace xUnitRevit
{
  class App : IExternalApplication
  {
    public Result OnStartup(UIControlledApplication a)
    {
#if DEBUG
      a.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized; ;
#endif

      return Result.Succeeded;
    }


    private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      Application app = sender as Application;
      UIApplication uiapp = new UIApplication(app);

      Runner.ReadConfig();

      if(Runner.Config.autoStart)
        Runner.Launch(uiapp);
    }


    public Result OnShutdown(UIControlledApplication a)
    {
      return Result.Succeeded;
    }
  }
}
