using WildFarm.IO.Interfaces;

namespace WildFarm.IO
{
    public class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();

    }
}
