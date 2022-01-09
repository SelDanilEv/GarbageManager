using GarbageManager.Consts;
using GarbageManager.Model.Result.Interfaces;
using System.Threading.Tasks;

namespace GarbageManager.Services.Interfaces
{
    interface ICleanUpProccessor
    {
       Task<IResultWithData<int>> StartCleanUp(string parentPath = AppConstants.HomePath);

        Task<IResultWithData<int>> RemoveFilesAndDirectories(string fileOrDirectoryName, string parentPath = AppConstants.HomePath);
    }
}
