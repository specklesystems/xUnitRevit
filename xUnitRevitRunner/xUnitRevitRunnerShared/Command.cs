#region Namespaces
using System.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace xUnitRevit
{
  [Transaction(TransactionMode.Manual)]
  public class Command : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements)
    {
      var uiapp = commandData.Application;
      Runner.Launch(uiapp);
      return Result.Succeeded;
    }
  }
}
