using GarbageManager.Enums;

namespace GarbageManager.Services.Interfaces
{
    interface IDirectoryProccessManager
    {
        int StartDirectoryCleanUp(string parentPath = SpecialPath.Home);
    }
}
