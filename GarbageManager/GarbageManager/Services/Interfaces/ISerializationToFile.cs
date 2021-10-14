using GarbageManager.Model;

namespace GarbageManager.Services.Interfaces
{
    interface ISerializationToFile
    {
        T ReadFileAndDeserialize<T>(GMFileInfo fileInfo) where T : new();
        bool WriteFile<T>(T model, GMFileInfo fileInfo);
        bool CreateFileIfNotExist(GMFileInfo fileInfo);
    }
}
