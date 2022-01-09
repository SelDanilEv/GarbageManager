using GarbageManager.Model.Result.Interfaces;
using System.Threading.Tasks;

namespace GarbageManager.Services.Interfaces
{
    interface IFileSystemCleanUp
    {
        Task<IResultWithData<int>> StartCleanUp();
        Task<IResultWithData<int>> RemoveFilesAndDirectories(string fileOrDirectoryName);
    }
}
