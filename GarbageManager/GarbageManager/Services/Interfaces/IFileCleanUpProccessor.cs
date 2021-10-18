using GarbageManager.Consts;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface IFileCleanUpProccessor
    {
        IResultWithData<int> StartFileCleanUp(string parentPath = AppConstants.HomePath);
    }
}
