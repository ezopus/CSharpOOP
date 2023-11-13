using Raiding.IO.Interfaces;

namespace Raiding.IO
{
    public class FileWriter : IWriter
    {
        public void WriteLine(object obj)
        {
            using StreamWriter writer = new("../../../output.txt", true);

            writer.WriteLine(obj);
        }
    }
}
