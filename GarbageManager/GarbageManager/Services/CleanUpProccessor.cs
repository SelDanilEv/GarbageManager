using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class CleanUpProccessor : ICleanUpProccessor
    {
        public IResultWithData<int> StartCleanUp(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder, parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.GetPath());

            int removedCounter = 0;
            try
            {
                removedCounter = CleanUpRecursive(homeDirectory, 0);
            }
            catch(Exception e)
            {
                return Result<int>.ErrorResult(ResultMessages.CleaningFailed);
            }

            return Result<int>.SuccessResult(removedCounter);
        }

        public int CleanUpRecursive(DirectoryInfo source, int removedCounter)
        {
            source.Refresh();

            int removedItems = 0;

            foreach (FileInfo fi in source.GetFiles())
            {
                if (fi.LastAccessTime.AddMonths(0) < DateTime.Now)
                {
                    removedItems++;
                    FileSystem.DeleteFile(fi.FullName, 
                                            UIOption.OnlyErrorDialogs,
                                            RecycleOption.SendToRecycleBin);
                }
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                CleanUpRecursive(diSourceSubDir, removedItems);

                if (diSourceSubDir.CreationTime.AddMonths(0) < DateTime.Now &&
                    diSourceSubDir.GetFiles().Length == 0 &&
                    diSourceSubDir.GetDirectories().Length == 0)
                {
                    removedItems++;
                    FileSystem.DeleteDirectory(diSourceSubDir.FullName,
                                            UIOption.OnlyErrorDialogs,
                                            RecycleOption.SendToRecycleBin);
                    diSourceSubDir.Delete(true);
                }
            }

            return removedItems + removedCounter;
        }
    }
}
