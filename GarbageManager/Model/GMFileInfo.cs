using GarbageManager.Enums;
using System;

namespace GarbageManager.Model
{
    public class GMFileInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public GMFileInfo()
        {
        }

        public GMFileInfo(string path, string name, string extension)
        {
            Path = path;
            Name = name;
            Extension = extension;
        }

        public override string ToString()
        {
            if (Path != SpecialPath.Relative && !Path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                Path += "/";
            }
            return $"${Path}{Name}.{Extension}";
        }
    }
}
