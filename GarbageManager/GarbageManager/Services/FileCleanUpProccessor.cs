using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class FileCleanUpProccessor : IFileCleanUpProccessor
    {
        public IResultWithData<int> StartFileCleanUp(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder, parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.GetPath());
            var files = homeDirectory.GetFiles();

            int removedFiles = 0;

            foreach (var file in files)
            {
                file.Refresh();
                if (file.LastAccessTime.AddMonths(1) < DateTime.Now)
                {
                    removedFiles++;
                    file.Delete();
                }
            }

            return Result<int>.SuccessResult(removedFiles);
        }
    }
}
