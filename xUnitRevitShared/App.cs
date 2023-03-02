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
      a.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;
      a.DialogBoxShowing += new EventHandler<Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs>(AppDialogShowing);
      return Result.Succeeded;
    }


    private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      Application app = sender as Application;
      UIApplication uiapp = new UIApplication(app);

      Runner.ReadConfig();

      if (Runner.Config.autoStart)
        Runner.Launch(uiapp);
    }

    private void AppDialogShowing(object sender, Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs e)
    {
      // don't show the dialog, just move on with life and testing
      e.OverrideResult(1);
    }


    public Result OnShutdown(UIControlledApplication a)
    {
      a.ControlledApplication.ApplicationInitialized -= ControlledApplication_ApplicationInitialized;
      a.DialogBoxShowing -= new EventHandler<Autodesk.Revit.UI.Events.DialogBoxShowingEventArgs>(AppDialogShowing);
      return Result.Succeeded;
    }
  }
}
