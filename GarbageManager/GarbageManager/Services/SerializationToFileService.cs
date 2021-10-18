using GarbageManager.Model;
using GarbageManager.Model.Result;
using GarbageManager.Model.Result.Interfaces;
using GarbageManager.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class SerializationToFileService : ISerializationToFile
    {
        public IResult CreateFileIfNotExist(GMFileInfo fileInfo)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileInfo.GetPath()));
            }
            catch (Exception e)
            {
                return Result.ErrorResult(e.Message);
            }

            return Result.SuccessResult();
        }

        public IResultWithData<T> ReadFileAndDeserialize<T>(GMFileInfo fileInfo) where T : new()
        {
            CreateFileIfNotExist(fileInfo);

            var result = new T();
            try
            {
                using (var sr = new StreamReader(fileInfo.GetPath()))
                {
                    var jsonResult = sr.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {
                return Result<T>.ErrorResult(e.Message);
            }

            return Result<T>.SuccessResult(result);
        }

        public IResult WriteFile<T>(T model, GMFileInfo fileInfo)
        {
            CreateFileIfNotExist(fileInfo);

            try
            {
                using (var sr = new StreamWriter(fileInfo.GetPath()))
                {
                    var json = JsonConvert.SerializeObject(model);
                    sr.Write(json);
                }
            }
            catch (Exception e)
            {
                return Result.ErrorResult(e.Message);
            }

            return Result.SuccessResult();
        }
    }
}
