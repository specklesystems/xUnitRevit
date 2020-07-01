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
using System.Web.Script.Serialization;
using System.IO;

namespace xUnitRevit
{
  /// <summary>
  /// Responsible for launching the xUnit WPF interface and initializing xru with Revit data
  /// </summary>
  public static class Runner
  {
    internal static Configuration Config = new Configuration();
    internal static void Launch(UIApplication uiapp)
    {
      try
      {
        var queue = new List<Action>();
        var eventHandler = ExternalEvent.Create(new ExternalEventHandler(queue));

        xru.Initialize(uiapp, SynchronizationContext.Current, eventHandler, queue);

        var main = new MainWindow();
        main.Title = "xUnit Revit by Speckle";

        //pre-load asssemblies, if you're a lazy developer
        (main.DataContext as MainViewModel).StartupAssemblies = Config.startupAssemblies;
        main.Show();


      }
      catch (Exception e)
      {
        //fail silently
      }
    }

    internal static void ReadConfig()
    {
      try
      {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(dir, "config.json");
        JavaScriptSerializer JavaScriptSerializer = new JavaScriptSerializer();
        var json = File.ReadAllText(path);
        Config = JavaScriptSerializer.Deserialize<Configuration>(json);
      }
      catch
      {}
    }
  }

}
