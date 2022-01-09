using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.Threading.Tasks;

namespace GarbageManager.Services
{
    class FileSystemCleanUp : IFileSystemCleanUp
    {
        private ICleanUpProccessor _cleanUpProccessor;

        public FileSystemCleanUp(ICleanUpProccessor cleanUpProccessor = null)
        {
            _cleanUpProccessor = new CleanUpProccessor();
        }

        public async Task<IResultWithData<int>> StartCleanUp()
        {
            if (!string.IsNullOrWhiteSpace(GMAppContext.StartAppSettings?.PathToGarbageFolder))
            {
                var cleanupResult = await _cleanUpProccessor.StartCleanUp();

                if (cleanupResult.IsSuccess)
                {
                    var totalEntitiesRemoved = cleanupResult.GetData;
                    GMAppContext.LastCheckingTime = DateTime.Now;
                    return  Result<int>.SuccessResult(totalEntitiesRemoved).BuildMessage(ResultMessages.CleaningCompletedSuccessfully, totalEntitiesRemoved.ToString());
                }
                else
                {
                    return Result<int>.ErrorResult(ResultMessages.CleaningFailed);
                }
            }

            return Result<int>.ErrorResult(ResultMessages.GarbageFolderIsNotFound);
        }

        public async Task<IResultWithData<int>> RemoveFilesAndDirectories(string fileOrDirectoryName)
        {
            if (!string.IsNullOrWhiteSpace(GMAppContext.StartAppSettings?.PathToGarbageFolder))
            {
                var cleanupResult = await _cleanUpProccessor.RemoveFilesAndDirectories(fileOrDirectoryName);

                if (cleanupResult.IsSuccess)
                {
                    var totalEntitiesRemoved = cleanupResult.GetData;
                    return Result<int>.SuccessResult(totalEntitiesRemoved).BuildMessage(ResultMessages.CleaningCompletedSuccessfully, totalEntitiesRemoved.ToString());
                }
                else
                {
                    return Result<int>.ErrorResult(ResultMessages.CleaningFailed);
                }
            }

            return Result<int>.ErrorResult(ResultMessages.GarbageFolderIsNotFound);
        }
    }
}
