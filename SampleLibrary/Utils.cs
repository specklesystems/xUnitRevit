using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
  public static class Utils
  {
    /// <summary>
    /// Utility method to get models from local folder rather than an absolute path
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public static string GetTestModel(string filename)
    {
      var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "TestModels", filename);
      return path;

    }
  }
}
