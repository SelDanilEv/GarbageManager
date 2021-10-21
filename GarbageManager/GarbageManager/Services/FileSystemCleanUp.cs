using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;

namespace GarbageManager.Services
{
    class FileSystemCleanUp : IFileSystemCleanUp
    {
        private ICleanUpProccessor _cleanUpProccessor;

        public FileSystemCleanUp(ICleanUpProccessor cleanUpProccessor = null)
        {
            _cleanUpProccessor = cleanUpProccessor ?? new CleanUpProccessor();
        }

        public IResultWithData<int> StartCleanUp()
        {
            if (!string.IsNullOrWhiteSpace(GMAppContext.StartAppSettings?.PathToGarbageFolder))
            {
                var cleanupResult = _cleanUpProccessor.StartCleanUp();

                IResultWithData<int> result = null;

                if (cleanupResult.IsSuccess)
                {
                    var totalLevel1EntitiesRemoved = cleanupResult.GetData;
                    result = Result<int>.SuccessResult(totalLevel1EntitiesRemoved).BuildMessage(ResultMessages.CleaningCompletedSuccessfully, totalLevel1EntitiesRemoved.ToString());
                    GMAppContext.LastCheckingTime = DateTime.Now;
                }
                else
                {
                    result = Result<int>.ErrorResult(ResultMessages.CleaningFailed);
                }

                return result;
            }

            return Result<int>.ErrorResult(ResultMessages.GarbageFolderIsNotFound);
        }
    }
}
