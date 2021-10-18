namespace GarbageManager.Model
{
    public class FileSystemPathString
    {
        public string BasePath { get; set; }
        public string RelativePath { get; set; }

        public FileSystemPathString()
        {
        }

        public FileSystemPathString(string basePath, string relativePath)
        {
            BasePath = basePath;
            RelativePath = relativePath;
        }

        public string GetPath()
        {
            if (!string.IsNullOrWhiteSpace(BasePath))
            {
                BasePath = BasePath.TrimEnd('/');
                BasePath += "/";
            }

            RelativePath = RelativePath.TrimStart('/');

            return $"{BasePath}{RelativePath}";
        }

        public override string ToString()
        {
            return this.GetPath();
        }
    }
}
