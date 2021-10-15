using GarbageManager.Model;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageManager.Services
{
    class FileProccessManager : IFileProccessManager
    {
        public int StartFileCleanUp(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder,parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.ToString());
            var files = homeDirectory.GetFiles();

            int removedFiles = 0;

            foreach(var file in files)
            {
                file.Refresh();
                if(file.LastWriteTime.AddMonths(1) < DateTime.Now)
                {
                    removedFiles++;
                    file.Delete();
                }
            }

            return removedFiles;
        }
    }
}
