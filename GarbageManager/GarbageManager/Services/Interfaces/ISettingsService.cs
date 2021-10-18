using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface ISettingsService
    {
        IResultWithData<StartAppSettings> GetStartAppSettings();
        IResultWithData<AppSettings> GetCommonSettings();
        IResult UpdateSettings(StartAppSettings startAppSettings);
        IResult UpdateSettings(AppSettings startAppSettings);
    }
}
