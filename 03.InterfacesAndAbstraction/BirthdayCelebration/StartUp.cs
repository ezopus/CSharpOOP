using BirthdayCelebration.Core.Interfaces;
using BirthdayCelebration.Core;

namespace BirthdayCelebration
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