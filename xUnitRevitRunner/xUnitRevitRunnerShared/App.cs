#region Namespaces
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

      string path = typeof(App).Assembly.Location;
      RibbonPanel ribbonPanel = a.CreateRibbonPanel("xUnitRevit by Speckle");

      var xUnitRevitButton = ribbonPanel.AddItem(new PushButtonData("Test Runner", "Test Runner", typeof(App).Assembly.Location, typeof(Command).FullName)) as PushButton;

      if (xUnitRevitButton != null)
      {
        xUnitRevitButton.Image = LoadPngImgSource("xUnitRevitRunner.Assets.icon16.png", path);
        xUnitRevitButton.LargeImage = LoadPngImgSource("xUnitRevitRunner.Assets.icon32.png", path);
        xUnitRevitButton.ToolTipImage = LoadPngImgSource("xUnitRevitRunner.Assets.icon32.png", path);
        xUnitRevitButton.ToolTip = "xUnit Test runner for Revit";
        xUnitRevitButton.AvailabilityClassName = typeof(CmdAvailabilityViews).FullName;
        xUnitRevitButton.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, "https://speckle.systems"));
      }


      return Result.Succeeded;
    }

    private void ControlledApplication_ApplicationInitialized(object sender, Autodesk.Revit.DB.Events.ApplicationInitializedEventArgs e)
    {
      var app = sender as Application;
      var uiapp = new UIApplication(app);

      Runner.ReadConfig();

      if (Runner.Config.AutoStart)
        Runner.Launch(uiapp);
    }

    public Result OnShutdown(UIControlledApplication a)
    {
      return Result.Succeeded;
    }

    private ImageSource LoadPngImgSource(string sourceName, string path)
    {
      try
      {
        var assembly = Assembly.LoadFrom(Path.Combine(path));
        var icon = assembly.GetManifestResourceStream(sourceName);
        PngBitmapDecoder m_decoder = new PngBitmapDecoder(icon, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
        ImageSource m_source = m_decoder.Frames[0];
        return (m_source);
      }
      catch { }

      return null;
    }
  }
}
