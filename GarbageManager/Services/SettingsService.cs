using GarbageManager.Model;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;

namespace GarbageManager.Services
{
    public class SettingsService : ISettingsService
    {
        private IFileService fileService;

        public SettingsService()
        {
            fileService = new FileService();
        }

        public AppSettings GetCommonSettings()
        {
            return fileService.ReadFileAndDeserialize<AppSettings>(GMAppContext.AppSettingsFileName);
        }

        public StartAppSettings GetStartAppSettings()
        {
            return fileService.ReadFileAndDeserialize<StartAppSettings>(GMAppContext.StartAppSettingsFileName);
        }

        public bool UpdateSettings(AppSettings settings)
        {
            return fileService.WriteFile(settings, GMAppContext.AppSettingsFileName);
        }

        public bool UpdateSettings(StartAppSettings settings)
        {
            return fileService.WriteFile(settings, GMAppContext.StartAppSettingsFileName);
        }
    }
}
