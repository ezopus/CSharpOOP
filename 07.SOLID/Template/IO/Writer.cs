using Template.IO.Interfaces;

namespace Template.IO
{
    public class Writer : IWriter
    {
        public void WriteLine(object obj) => Console.WriteLine(obj);
    }
}
