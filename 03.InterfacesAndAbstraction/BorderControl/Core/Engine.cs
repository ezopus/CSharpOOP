using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BorderControl.Core.Interfaces;
using BorderControl.Models;
using BorderControl.Models.Interfaces;

namespace BorderControl.Core
{
    public class Engine : IEngine
    {
        private List<IHabitant> habitants;
        public void Run()
        {
            habitants = new List<IHabitant>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                switch (tokens.Length)
                {
                    case 2:
                        IRobot robot = new Robot(tokens[0], tokens[1]);
                        habitants.Add(robot);
                        break;
                    case 3:
                        ICitizen citizen = new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2]);
                        habitants.Add(citizen);
                        break;
                    default:
                        break;
                }
            }

            string filter = Console.ReadLine();

            PrintFiltered(filter,  habitants);

        }

        private void PrintFiltered(string filter, List<IHabitant> habitants)
        {
            foreach (var habitant in habitants)
            {
                if (habitant.Id.EndsWith(filter))
                {
                    Console.WriteLine(habitant.Id);
                }
            }
        }
    }
}
