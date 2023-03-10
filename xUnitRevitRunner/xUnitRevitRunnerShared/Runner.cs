using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Script.Serialization;
using Autodesk.Revit.UI;
using Xunit.Runner.Wpf;
using Xunit.Runner.Wpf.ViewModel;
using xUnitRevitUtils;

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

        var main = new MainWindow
        {
          Title = "xUnit Revit Runner by Speckle",
          MaxHeight = 800
        };

        //pre-load asssemblies, if you're a lazy developer
        if (main.DataContext is MainViewModel mainViewModel)
          mainViewModel.StartupAssemblies = Config.StartupAssemblies.ToList();
        main.Show();
      }
      catch
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
        var JavaScriptSerializer = new JavaScriptSerializer();
        var json = File.ReadAllText(path);
        Config = JavaScriptSerializer.Deserialize<Configuration>(json);
      }
      catch { }
    }
  }
}
