using Template.IO.Interfaces;

namespace Template.IO
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
