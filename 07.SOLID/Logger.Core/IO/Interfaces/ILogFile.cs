namespace Logger.Core.IO.Interfaces
{
    public interface ILogFile
    {
        string Name { get; }
        string Path { get; }
        string Extension { get; }
        string FullPath { get; }
        int Size { get; }
        void Write();

    }
}
