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
    public List<string> startupAssemblies19 { get; set; } = new List<string>();
    public List<string> startupAssemblies20 { get; set; } = new List<string>();
    public List<string> startupAssemblies21 { get; set; } = new List<string>();
    public List<string> startupAssemblies22 { get; set; } = new List<string>();
    public List<string> startupAssemblies23 { get; set; } = new List<string>();
    public bool autoStart { get; set; } = false;
  }
}
