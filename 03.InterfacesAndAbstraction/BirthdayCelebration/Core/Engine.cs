using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayCelebration.Core.Interfaces;
using BirthdayCelebration.Models.Interfaces;
using BirthdayCelebration.Models;

namespace BirthdayCelebration.Core
{
    public class Engine : IEngine
    {
        private List<IBirthable> habitants;
        public void Run()
        {
            habitants = new List<IBirthable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                switch (tokens[0])
                {
                    case "Pet":
                        IBirthable pet = new Pet(tokens[1], tokens[2]);
                        habitants.Add(pet);
                        break;
                    case "Citizen":
                        IBirthable citizen = new Citizen(tokens[1], int.Parse(tokens[2]), tokens[3], tokens[4]);
                        habitants.Add(citizen);
                        break;
                }
            }

            string filter = Console.ReadLine();

            PrintFiltered(filter, habitants);

        }

        private void PrintFiltered(string filter, List<IBirthable> habitants)
        {
            foreach (var habitant in habitants)
            {
                if (habitant.Birthdate.EndsWith(filter))
                {
                    Console.WriteLine(habitant.Birthdate);
                }
            }
        }
    }
}
