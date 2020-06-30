using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitRevit
{
  /// <summary>
  /// Simple configuration file for lazy developers
  /// </summary>
  public class Configuration
  {
    public List<string> startupAssemblies { get; set; } = new List<string>();
    public bool autoStart { get; set; } = false;
  }
}
