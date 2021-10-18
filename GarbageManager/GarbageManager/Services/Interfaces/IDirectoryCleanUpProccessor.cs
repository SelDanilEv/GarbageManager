using GarbageManager.Consts;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface IDirectoryCleanUpProccessor
    {
       IResultWithData<int> StartDirectoryCleanUp(string parentPath = AppConstants.HomePath);
    }
}
