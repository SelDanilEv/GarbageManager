using GarbageManager.Model;
using System;

namespace GarbageManager.Singleton
{
    public static class GMAppContext
    {
        public static StartAppSettings StartAppSettings { get; set; }
        public static DateTime LastCheckingTime { get; set; }

        static GMAppContext()
        {
            StartAppSettings = new StartAppSettings();
        }
    }
}
