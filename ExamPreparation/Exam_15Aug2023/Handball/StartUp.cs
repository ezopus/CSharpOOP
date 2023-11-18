using Handball.Core;
using Handball.Core.Contracts;

namespace Handball
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();

            //Goalkeeper player = new("Ivan");
            //CenterBack center = new("Gosho");
            //ForwardWing forward = new("Stefan");
            //Team rockets = new Team("Rockets");

            //rockets.SignContract(player);
            //rockets.SignContract(center);
            //rockets.SignContract(forward);

            //Console.WriteLine(rockets);
            //Console.WriteLine();

            //rockets.Draw();
            //Console.WriteLine(rockets);
            //Console.WriteLine();

            //rockets.Lose();
            //rockets.Lose();
            //rockets.Lose();
            //rockets.Lose();
            //rockets.Lose();
            //rockets.Lose();
            //rockets.Lose();
            //Console.WriteLine(rockets);
            //Console.WriteLine();
        }
    }
}
