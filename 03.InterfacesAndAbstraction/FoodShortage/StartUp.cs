using FoodShortage.Models;
using FoodShortage.Models.Interfaces;

namespace FoodShortage
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, IBuyer> buyers = new();

            int numberOfBuyers = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfBuyers; i++)
            {
                string[] input = Console.ReadLine().Split(" ");
                if (input.Length == 3)
                {
                    Rebel rebel = new Rebel(input[0], int.Parse(input[1]), input[2]);
                    buyers.Add(input[0], rebel);
                }
                else
                {
                    Citizen citizen = new Citizen(input[0], int.Parse(input[1]), input[2], input[3]);
                    buyers.Add(input[0], citizen);
                }
            }

            string inputNames;
            
            while ((inputNames = Console.ReadLine()) != "End")
            {
                if (buyers.ContainsKey(inputNames))
                {
                    buyers[inputNames].BuyFood();
                }
            }

            Console.WriteLine(buyers.Sum(b => b.Value.Food));
        }
    }
}