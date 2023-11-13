using Raiding.IO.Interfaces;

namespace Raiding.IO
{
    public class Writer : IWriter
    {
        public void WriteLine(object obj) => Console.WriteLine(obj);
    }
}
