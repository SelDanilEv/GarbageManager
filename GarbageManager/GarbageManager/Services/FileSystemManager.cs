using GarbageManager.Enums;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageManager.Services
{
    class FileSystemManager : IFileSystemManager
    {
        private IFileProccessManager _fileProccessManager; 
        private IDirectoryProccessManager _directoryProccessManager;

        public FileSystemManager(IFileProccessManager fileProccessManager = null, IDirectoryProccessManager directoryProccessManager = null)
        {
            _fileProccessManager = fileProccessManager ?? new FileProccessManager();
            _directoryProccessManager = directoryProccessManager ?? new DirectoryProccessManager();
        }

        public void StartCleanUp()
        {
            if (!string.IsNullOrWhiteSpace(GMAppContext.StartAppSettings?.PathToGarbageFolder))
            {
                _fileProccessManager.StartFileCleanUp();
                _directoryProccessManager.StartDirectoryCleanUp();

                GMAppContext.LastCheckingTime = DateTime.Now;
            }
        }
    }
}
