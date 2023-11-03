using BorderControl.Core;
using BorderControl.Core.Interfaces;

namespace BorderControl
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}