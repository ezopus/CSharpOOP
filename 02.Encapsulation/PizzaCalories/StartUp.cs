using PizzaCalories.Models;

namespace PizzaCalories
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                string[] pizzaTokens = Console.ReadLine().Split();
                string[] doughTokens = Console.ReadLine().Split();
                Dough dough = new Dough(doughTokens[1], doughTokens[2], double.Parse(doughTokens[3]));
                Pizza pizza = new Pizza(pizzaTokens[1], dough);
                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] toppingTokens = input.Split();
                    Topping topping = new Topping(toppingTokens[1], double.Parse(toppingTokens[2]));
                    pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}