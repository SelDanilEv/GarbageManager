using System;

namespace GarbageManager.Model
{
    public class GMFileInfo : ICloneable
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public bool IsFile { get; set; }

        public GMFileInfo() { }

        public GMFileInfo(string path, string name, string extension, bool isFile = true)
        {
            Path = path;
            Name = name;
            Extension = extension;
            IsFile = isFile;
        }

        public string GetPath()
        {
            var relativePath = new FileSystemPathString(Path, Name);
            var extesnion = string.Empty;
            if (!string.IsNullOrWhiteSpace(Extension) && IsFile)
            {
                extesnion = "." + Extension;
            }
            return $"{relativePath.GetPath()}{extesnion}";
        }

        public override string ToString()
        {
            return this.GetPath();
        }

        public object Clone()
        {
            return new GMFileInfo(Path, Name, Extension, IsFile);
        }
    }
}
