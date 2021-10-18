using GarbageManager.Consts;
using GarbageManager.Model;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;

namespace GarbageManager.Services
{
    public class SettingsService : ISettingsService
    {
        private ISerializationToFile fileService;

        public SettingsService()
        {
            fileService = new SerializationToFileService();
        }

        public IResultWithData<AppSettings> GetCommonSettings()
        {
            var result = fileService.ReadFileAndDeserialize<AppSettings>(AppConstants.AppSettingsFileName);
            return result;
        }

        public IResultWithData<StartAppSettings> GetStartAppSettings()
        {
            var result = fileService.ReadFileAndDeserialize<StartAppSettings>(AppConstants.StartAppSettingsFileName);
            return result;
        }

        public IResult UpdateSettings(AppSettings settings)
        {
            return fileService.WriteFile(settings, AppConstants.AppSettingsFileName);
        }

        public IResult UpdateSettings(StartAppSettings settings)
        {
            return fileService.WriteFile(settings, AppConstants.StartAppSettingsFileName);
        }
    }
}
