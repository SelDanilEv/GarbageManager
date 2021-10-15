using GarbageManager.Enums;

namespace GarbageManager.Services.Interfaces
{
    interface IFileProccessManager
    {
        int StartFileCleanUp(string parentPath = SpecialPath.Home);
    }
}
