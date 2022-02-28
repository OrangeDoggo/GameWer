using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace GameWer.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) Synchronized(new Settings());

    public static Settings Default
    {
      get
      {
        var defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
