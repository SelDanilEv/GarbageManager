using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class DirectoryCleanUpProccessor : IDirectoryCleanUpProccessor
    {
        public IResultWithData<int> StartDirectoryCleanUp(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder, parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.GetPath());
            var directories = homeDirectory.GetDirectories();

            int removedDirectories = 0;

            foreach (var directory in directories)
            {
                directory.Refresh();
                if (directory.LastAccessTime.AddMonths(1) < DateTime.Now)
                {
                    removedDirectories++;
                    directory.Delete(true);
                }
            }

            return Result<int>.SuccessResult(removedDirectories);
        }

        public void CopyRecursive(DirectoryInfo source)
        {
            foreach (FileInfo fi in source.GetFiles())
            {
                // proccess files
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                // proccess directory
                CopyRecursive(diSourceSubDir);
            }
        }
    }
}
