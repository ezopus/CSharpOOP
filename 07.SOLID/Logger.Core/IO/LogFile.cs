using Logger.Core.Exceptions;
using Logger.Core.IO.Interfaces;

namespace Logger.Core.IO
{
    public class LogFile : ILogFile
    {
        private const string DefaultExtension = "txt";
        private static readonly string DefaultName = $"Log_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
        private static readonly string DefaultPath = $"{Directory.GetCurrentDirectory()}";
        private string name;
        private string path;
        private string extension;

        public LogFile()
        {
            Extension = DefaultExtension;
            Name = DefaultName;
            Path = DefaultPath;

        }
        public LogFile(string name, string path, string extension)
            : this()
        {
            Name = name;
            Path = path;
            Extension = extension;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EmptyFileName();
                }
                name = value;
            }

        }
        public string Path
        {
            get => path;
            set
            {
                if (!Directory.Exists(value))
                {
                    throw new InvalidPathException();
                }
                path = value;
            }
        }

        public string Extension
        {
            get => extension;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new EmptyFileExtension();
                }
                extension = value;
            }
        }
        public string FullPath => System.IO.Path.GetFullPath($"{Path}/{Name}.{Extension}");
        public int Size => File.ReadAllText(FullPath).Length;
        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
