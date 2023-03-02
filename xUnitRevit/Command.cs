#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Threading;
#endregion

namespace xUnitRevit {
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand {
        static object consoleLock = new object();
        static ManualResetEvent finished = new ManualResetEvent(false);
        static Result result = Result.Succeeded;

        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements) {
            UIApplication uiapp = commandData.Application;
            Runner.Launch(uiapp);
            return Result.Succeeded;
        }
    }


}
