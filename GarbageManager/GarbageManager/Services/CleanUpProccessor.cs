using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GarbageManager.Consts;
using GarbageManager.Helpers;
using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using GarbageManager.Singleton;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

namespace GarbageManager.Services
{
    // must be new instance for each call
    class CleanUpProccessor : ICleanUpProccessor
    {
        private ConcurrentBag<Task<int>> _taskList = new ConcurrentBag<Task<int>>();

        private static Task<int> _blockerTask;
        private static readonly object _locker = new object();

        public static Task<int> BlockerTask
        {
            get
            {
                if (_blockerTask == null)
                {
                    lock (_locker)
                    {
                        if (_blockerTask == null)
                            _blockerTask = Task<int>.FromResult(0);
                    }
                }

                return _blockerTask;
            }
            set
            {
                _blockerTask = value;
            }
        }

        public async Task<IResultWithData<int>> StartCleanUp(string parentPath = AppConstants.HomePath)
        {
            var homeDirectory = HomeDirectoryHandling(parentPath);

            try
            {
                _taskList.Add(Task.Run(async () =>
                    await CleanUpRecursive(homeDirectory, (info) => info.LastAccessTime.AddMonths(1) < DateTime.Now)));
                _taskList.Add(Task.Run(async () =>
                    await CleanUpRecursive(homeDirectory, (info) =>
                                            info is DirectoryInfo &&
                                            info.CreationTime.AddMonths(2) < DateTime.Now &&
                                            ((DirectoryInfo)info).GetFiles().Length == 0 &&
                                            ((DirectoryInfo)info).GetDirectories().Length == 0)));
            }
            catch
            {
                return Result<int>.ErrorResult(ResultMessages.CleaningFailed);
            }

            var removedItemsCount = await AddAsynchronousResults();

            return Result<int>.SuccessResult(removedItemsCount);
        }

        public async Task<IResultWithData<int>> RemoveFilesAndDirectories(string fileOrDirectoryName, string parentPath = AppConstants.HomePath)
        {
            var homeDirectory = HomeDirectoryHandling(parentPath);

            try
            {
                _taskList.Add(Task.Run(async () =>
                    await CleanUpRecursive(homeDirectory, (info) => info.Name == fileOrDirectoryName)));
            }
            catch (Exception e)
            {
                return Result<int>.ErrorResult(ResultMessages.CleaningFailed);
            }

            var removedItemsCount = await AddAsynchronousResults();

            return Result<int>.SuccessResult(removedItemsCount);
        }

        private DirectoryInfo HomeDirectoryHandling(string parentPath)
        {
            var relativePath = new FileSystemPathString(GMAppContext.StartAppSettings.PathToGarbageFolder, parentPath);
            var homeDirectory = new DirectoryInfo(relativePath.GetPath());

            return homeDirectory;
        }

        private async Task<int> CleanUpRecursive(DirectoryInfo source, Func<FileSystemInfo, bool> condition = null)
        {
            Task.WaitAll(BlockerTask);
            source.Refresh();

            int removedCounter = 0;

            foreach (FileInfo fi in source.GetFiles())
            {
                if (condition != null && condition(fi))
                {
                    removedCounter++;

                    TaskHelper.RunBg(() =>
                    {
                        FileSystem.DeleteFile(fi.FullName,
                            UIOption.OnlyErrorDialogs,
                            RecycleOption.SendToRecycleBin);
                    });
                }
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                if (condition != null && condition(diSourceSubDir))
                {
                    removedCounter += diSourceSubDir.GetFiles("*", SearchOption.AllDirectories).Length + 1;

                    TaskHelper.RunBg(() =>
                    {
                        FileSystem.DeleteDirectory(diSourceSubDir.FullName,
                                                UIOption.OnlyErrorDialogs,
                                                RecycleOption.SendToRecycleBin);
                    });

                }

                if (diSourceSubDir.Name != "node_modules")
                {
                    _taskList.Add(CleanUpRecursive(diSourceSubDir, condition));
                }
            }

            return removedCounter;
        }

        private async Task<int> AddAsynchronousResults()
        {
            var result = 0;

            int timeCounter = 0;

            do
            {
                if (_taskList.Count > 100 || timeCounter == 99)
                {
                    BlockerTask = ClearTaskList();
                    result += await BlockerTask;

                    if (timeCounter < 99)
                    {
                        timeCounter = 0;
                    }
                }

                Thread.Sleep(10);
            }
            while (timeCounter++ < 100);

            return result;
        }

        public async Task<int> ClearTaskList()
        {
            var list = _taskList.Where(x => x.IsCompleted).ToList();

            _taskList.Clear();

            var results = await Task.WhenAll<int>(list);

            return results.Sum();
        }
    }
}
