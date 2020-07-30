using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using xUnitRevitUtils;

namespace SampleLibrary
{
  public class SampleTest
  {
    /// <summary>
    /// Checks wether all walls in the model have a valid volume
    /// </summary>
    [Fact]
    public void WallsHaveVolume()
    {
      var testModel = Utils.GetTestModel("walls.rvt");
      var doc = xru.OpenDoc(testModel);

      var walls = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls).ToElements();

      foreach(var wall in walls)
      {
        var volumeParam = wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
        Assert.NotNull(volumeParam);
        Assert.True(volumeParam.AsDouble() > 0);
      }
    }

    [Fact]
    public void SampleFail()
    {
      var feet = UnitUtils.ConvertToInternalUnits(3000, DisplayUnitType.DUT_MILLIMETERS);
      Assert.Equal(5, feet);
    }


  }
}
