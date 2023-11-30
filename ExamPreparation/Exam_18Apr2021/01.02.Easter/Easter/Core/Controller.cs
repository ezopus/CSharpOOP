using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnies;
        private EggRepository eggs;
        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType != nameof(HappyBunny) && bunnyType != nameof(SleepyBunny))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            IBunny bunny;
            if (bunnyType == nameof(HappyBunny))
            {
                bunny = new HappyBunny(bunnyName);
            }
            else
            {
                bunny = new SleepyBunny(bunnyName);
            }

            bunnies.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            if (bunnies.FindByName(bunnyName) == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentBunny));
            }

            IBunny bunny = bunnies.FindByName(bunnyName);
            bunny.AddDye(new Dye(power));
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg currentEgg = eggs.FindByName(eggName);
            var bunniesColoring = bunnies.Models.Where(b => b.Energy >= 50).OrderByDescending(b => b.Energy).ToList();
            if (bunniesColoring.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }

            while (!currentEgg.IsDone() && bunniesColoring.Any())
            {
                Workshop workshop = new Workshop();
                IBunny currentBunny = bunniesColoring.FirstOrDefault();
                if (currentBunny == null)
                {
                    break;
                }
                workshop.Color(currentEgg, bunniesColoring.FirstOrDefault());
                if (currentBunny.Energy == 0 || currentBunny.Dyes.All(d => d.IsFinished()))
                {
                    bunniesColoring.Remove(currentBunny);
                }
                if (currentEgg.IsDone())
                {
                    break;
                }
            }

            if (currentEgg.IsDone())
            {
                return string.Format(OutputMessages.EggIsDone, currentEgg.Name);
            }
            else
            {
                return string.Format(OutputMessages.EggIsNotDone, currentEgg.Name);
            }
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{eggs.Models.Count(e => e.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach (var b in bunnies.Models)
            {
                sb.AppendLine($"Name: {b.Name}");
                sb.AppendLine($"Energy: {b.Energy}");
                sb.AppendLine($"Dyes: {b.Dyes.Count(d => !d.IsFinished())} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
