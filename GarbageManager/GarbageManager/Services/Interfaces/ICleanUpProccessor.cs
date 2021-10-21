using GarbageManager.Consts;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface ICleanUpProccessor
    {
       IResultWithData<int> StartCleanUp(string parentPath = AppConstants.HomePath);
    }
}
