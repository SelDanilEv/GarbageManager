using GarbageManager.Enums;
using System;

namespace GarbageManager.Model
{
    public class FileSystemPathString
    {
        public string BasePath { get; set; }
        public string RelativePath { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !BasePath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                BasePath += "/";
            }
            if (RelativePath.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                RelativePath = RelativePath.TrimStart('/');
            }
            return $"{BasePath}{RelativePath}";
        }
        
        public FileSystemPathString()
        {
        }

        public FileSystemPathString(string basePath, string relativePath)
        {
            BasePath = basePath;
            RelativePath = relativePath;
        }
    }
}
