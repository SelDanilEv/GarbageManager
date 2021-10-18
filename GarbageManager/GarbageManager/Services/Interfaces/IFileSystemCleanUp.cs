using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface IFileSystemCleanUp
    {
        IResultWithData<int> StartCleanUp();
    }
}
