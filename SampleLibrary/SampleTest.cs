using Autodesk.Revit.DB;
using Autodesk.Revit.DB.IFC;
using System.Linq;
using Xunit;
using xUnitRevitUtils;

namespace SampleLibrary {
    public class SampleTest {
        /// <summary>
        /// Checks whether all walls in the model have a valid volume
        /// </summary>
        [Fact]
        public void WallsHaveVolume() {
            var testModel = Utils.GetTestModel("walls.rvt");
            var doc = xru.OpenDoc(testModel);

            var walls = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls).ToElements();

            foreach (var wall in walls) {
                var volumeParam = wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
                Assert.NotNull(volumeParam);
                Assert.True(volumeParam.AsDouble() > 0);
            }
        }

        [Fact]
        public void SampleFail() {
#if pre2021
            var feet = UnitUtils.ConvertToInternalUnits(3000, DisplayUnitType.DUT_MILLIMETERS);
#else
            var feet = UnitUtils.ConvertToInternalUnits(3000, UnitTypeId.Feet);
#endif
            Assert.Equal(5, feet);
        }

        [Fact]
        public void GetWallGrossAreaAndRollBack() {
            var testModel = Utils.GetTestModel("walls.rvt");
            var doc = xru.OpenDoc(testModel);
            var walls = new FilteredElementCollector(doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Walls).ToElements();
            var wall = walls[0] as Wall;
            double grossArea = 0;

            var inserts = wall.FindInserts(true, true, true, true);
            xru.Run(() => {
                using (Transaction transaction = new Transaction(doc, "Temporary - only to get gross area")) {
                    transaction.Start();
                    foreach (ElementId insertId in inserts) { doc.Delete(insertId); }
                    doc.Regenerate();
                    var wallFaceReference = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior);
                    var face = doc.GetElement(wallFaceReference.First()).GetGeometryObjectFromReference(wallFaceReference.First()) as PlanarFace;
                    var wallFaceEdges = face.GetEdgesAsCurveLoops();
                    grossArea = ExporterIFCUtils.ComputeAreaOfCurveLoops(wallFaceEdges);
                    transaction.RollBack();
                }
            }, doc).Wait();
            Assert.True(grossArea > 0);
        }
    }
}
