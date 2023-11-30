using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System.Linq;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {
        }
        public void Color(IEgg egg, IBunny bunny)
        {
            while (bunny.Dyes.Any(d => !d.IsFinished()) && bunny.Energy > 0)
            {
                IDye currentDye = bunny.Dyes.FirstOrDefault(d => !d.IsFinished());
                while (!egg.IsDone() && !currentDye.IsFinished())
                {
                    currentDye.Use();
                    bunny.Work();
                    egg.GetColored();
                }

                if (egg.IsDone() || bunny.Dyes.All(d => d.IsFinished()))
                {
                    break;
                }
            }
        }
    }
}
