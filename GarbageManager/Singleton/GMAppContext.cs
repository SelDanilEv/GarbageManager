using GarbageManager.Enums;
using GarbageManager.Model;

namespace GarbageManager.Singleton
{
    public static class GMAppContext
    {
        public static readonly GMFileInfo StartAppSettingsFileName = new GMFileInfo(SpecialPath.Relative, "start", "bin");
        public static readonly GMFileInfo AppSettingsFileName = new GMFileInfo(SpecialPath.Relative, "settings", "bin");
        public static StartAppSettings StartAppSettings { get; set; }

        static GMAppContext()
        {
            StartAppSettings = new StartAppSettings();
        }
    }
}
