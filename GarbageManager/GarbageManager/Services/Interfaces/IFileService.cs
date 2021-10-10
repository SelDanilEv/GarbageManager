using GarbageManager.Model;

namespace GarbageManager.Services.Interfaces
{
    interface IFileService
    {
        T ReadFileAndDeserialize<T>(GMFileInfo fileInfo) where T : new();
        bool WriteFile<T>(T model, GMFileInfo fileInfo);
        bool CreateFileIfNotExist(GMFileInfo fileInfo);
    }
}
