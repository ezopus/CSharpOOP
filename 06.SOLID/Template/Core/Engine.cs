using Template.Core.Interfaces;
using Template.IO.Interfaces;

namespace Template.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        public Engine(IReader reader, IWriter writer)
        {
            this.writer = writer;
            this.reader = reader;
        }
        public void Run()
        {

        }

    }
}



