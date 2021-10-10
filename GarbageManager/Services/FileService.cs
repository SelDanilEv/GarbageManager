using GarbageManager.Model;
using GarbageManager.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GarbageManager.Services
{
    class FileService : IFileService
    {
        public bool CreateFileIfNotExist(GMFileInfo fileInfo)
        {
            try
            {
                using (StreamWriter w = File.AppendText(fileInfo.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public T ReadFileAndDeserialize<T>(GMFileInfo fileInfo) where T : new()
        {
            CreateFileIfNotExist(fileInfo);

            var result = new T();
            try
            {
                using (var sr = new StreamReader(fileInfo.ToString()))
                {
                    var jsonResult = sr.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public bool WriteFile<T>(T model, GMFileInfo fileInfo)
        {
            CreateFileIfNotExist(fileInfo);

            try
            {
                using (var sr = new StreamWriter(fileInfo.ToString()))
                {
                    var json = JsonConvert.SerializeObject(model);
                    sr.Write(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
