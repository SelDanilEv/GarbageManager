using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;

namespace GarbageManager.Services
{
    class FileSystemCleanUp : IFileSystemCleanUp
    {
        private IFileCleanUpProccessor _fileProccessManager; 
        private IDirectoryCleanUpProccessor _directoryProccessManager;

        public FileSystemCleanUp(IFileCleanUpProccessor fileProccessManager = null, IDirectoryCleanUpProccessor directoryProccessManager = null)
        {
            _fileProccessManager = fileProccessManager ?? new FileCleanUpProccessor();
            _directoryProccessManager = directoryProccessManager ?? new DirectoryCleanUpProccessor();
        }

        public IResultWithData<int> StartCleanUp()
        {
            if (!string.IsNullOrWhiteSpace(GMAppContext.StartAppSettings?.PathToGarbageFolder))
            {
                var fileCleanupResult = _fileProccessManager.StartFileCleanUp();
                var directoryCleanupResult = _directoryProccessManager.StartDirectoryCleanUp();

                IResultWithData<int> result = null;

                if (fileCleanupResult.IsSuccess && directoryCleanupResult.IsSuccess)
                {
                    var totalLevel1EntitiesRemoved = fileCleanupResult.GetData + directoryCleanupResult.GetData;
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
