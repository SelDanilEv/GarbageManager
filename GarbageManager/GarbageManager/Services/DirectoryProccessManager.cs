using GarbageManager.Model;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class DirectoryProccessManager : IDirectoryProccessManager
    {
        public int StartDirectoryCleanUp(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder, parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.ToString());
            var directories = homeDirectory.GetDirectories();

            int removedDirectories = 0;

            foreach (var directory in directories)
            {
                directory.Refresh();
                if (directory.LastWriteTime.AddMonths(1) < DateTime.Now)
                {
                    removedDirectories++;
                    directory.Delete(true);
                }
            }

            return removedDirectories;
        }
    }
}
