using GarbageManager.Model;
using GarbageManager.Model.Result.Interfaces;

namespace GarbageManager.Services.Interfaces
{
    interface ISerializationToFile
    {
        IResultWithData<T> ReadFileAndDeserialize<T>(GMFileInfo fileInfo) where T : new();
        IResult WriteFile<T>(T model, GMFileInfo fileInfo);
        IResult CreateFileIfNotExist(GMFileInfo fileInfo);
    }
}
