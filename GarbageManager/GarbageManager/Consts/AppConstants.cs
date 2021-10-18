using GarbageManager.Model;
using System;

namespace GarbageManager.Consts
{
    public static class AppConstants
    {
        public const string HomePath = "";
        public const string CustomFormatPath = "{0}";
        public const string ParentPath = "../";

        public static readonly GMFileInfo StartUpdateAppFile = new GMFileInfo(HomePath, "Setup", "exe");

        public static readonly GMFileInfo StartAppSettingsFileName = new GMFileInfo(HomePath, "start", "txt");
        public static readonly GMFileInfo AppSettingsFileName = new GMFileInfo(HomePath, "settings", "txt");
    }
}
