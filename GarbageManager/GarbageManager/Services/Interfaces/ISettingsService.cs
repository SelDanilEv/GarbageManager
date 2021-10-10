using GarbageManager.Model;

namespace GarbageManager.Services.Interfaces
{
    interface ISettingsService
    {
        StartAppSettings GetStartAppSettings();
        AppSettings GetCommonSettings();
        bool UpdateSettings(StartAppSettings startAppSettings);
        bool UpdateSettings(AppSettings startAppSettings);
    }
}
