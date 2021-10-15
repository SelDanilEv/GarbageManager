using GarbageManager.Enums;
using GarbageManager.Model;
using System;

namespace GarbageManager.Singleton
{
    public static class GMAppContext
    {
        public static readonly GMFileInfo StartAppSettingsFileName = new GMFileInfo(SpecialPath.Parent, "start", "txt");
        public static readonly GMFileInfo AppSettingsFileName = new GMFileInfo(SpecialPath.Parent, "settings", "txt");
        public static StartAppSettings StartAppSettings { get; set; }
        public static DateTime LastCheckingTime { get; set; }

        static GMAppContext()
        {
            StartAppSettings = new StartAppSettings();
        }
    }
}
