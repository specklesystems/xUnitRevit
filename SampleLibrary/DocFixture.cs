using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using xUnitRevitUtils;

namespace SampleLibrary
{
  public sealed class DocFixture : IDisposable
  {
    public Document Doc { get; set; }
    public IList<Element> Walls { get; set; }


    public DocFixture()
    {
      var testModel = Utils.GetTestModel("walls.rvt");
      Doc = xru.OpenDoc(testModel);

      Walls = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls).ToElements();
    }

    public void Dispose()
    {
    }
  }
}
