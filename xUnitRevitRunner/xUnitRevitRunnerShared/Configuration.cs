using System.Collections.Generic;

namespace xUnitRevit
{
  /// <summary>
  /// Simple configuration file for lazy developers
  /// </summary>
  public class Configuration
  {
    public IList<string> StartupAssemblies { get; set; } = new List<string>();
    public bool AutoStart { get; set; } = false;
  }
}
