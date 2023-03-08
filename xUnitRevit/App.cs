#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
#endregion

namespace xUnitRevit
{
  class App : IExternalApplication
  {
    public Result OnStartup(UIControlledApplication a)
    {
      a.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;

      return Result.Succeeded;
    }

    private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      var app = sender as Application;
      using var uiapp = new UIApplication(app);

      Runner.ReadConfig();

      if (Runner.Config.AutoStart)
        Runner.Launch(uiapp);
    }

    public Result OnShutdown(UIControlledApplication a)
    {
      return Result.Succeeded;
    }
  }
}
