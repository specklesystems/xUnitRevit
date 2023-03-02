using Autodesk.Revit.DB;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using xUnitRevitUtils;

namespace SampleLibrary {
    public class TestWithFixture : IClassFixture<DocFixture>
  {
    DocFixture fixture; 
    public TestWithFixture(DocFixture fixture)
    {
      this.fixture = fixture;
    }

    [Fact]
    public void CountWalls()
    {
      Assert.Equal(4, fixture.Walls.Count);
    }

    [Fact]
    public void WallOffset()
    {
      var wall = fixture.Doc.GetElement(new ElementId(346573));
      var param = wall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);

#if pre2021
            var baseOffset = UnitUtils.ConvertFromInternalUnits(param.AsDouble(), param.DisplayUnitType);
#else
            var baseOffset = UnitUtils.ConvertFromInternalUnits(param.AsDouble(), param.GetUnitTypeId());
#endif

            Assert.Equal(2000, baseOffset);
    }

    [Fact]
    public void MoveWallsUp()
    {
      var walls = fixture.Walls.Where(x => x.Id.IntegerValue != 346573);

      xru.RunInTransaction(() =>
      {
        foreach(var wall in walls)
        {
          var param = wall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);

#if pre2021
            var baseOffset = UnitUtils.ConvertToInternalUnits(2000, param.DisplayUnitType);
#else
            var baseOffset = UnitUtils.ConvertToInternalUnits(2000, param.GetUnitTypeId());
#endif
              param.Set(baseOffset);
        }
      }, fixture.Doc)
      .Wait(); // Important! Wait for action to finish

      foreach (var wall in walls)
      {
        var param = wall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
#if pre2021
            var baseOffset = UnitUtils.ConvertFromInternalUnits(param.AsDouble(), param.DisplayUnitType);
#else
                var baseOffset = UnitUtils.ConvertFromInternalUnits(param.AsDouble(), param.GetUnitTypeId());
#endif
                Assert.Equal(2000, baseOffset);
      }
    }
  }
}
