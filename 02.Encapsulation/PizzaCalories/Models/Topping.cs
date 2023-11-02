

namespace PizzaCalories.Models
{
    public class Topping
    {
        private const double BaseCaloriesPerGram = 2;
        private Dictionary<string, double> toppingType;
        private double weight;
        private string name;

        public Topping(string name, double weight)
        {
            toppingType = new Dictionary<string, double>
            {
                {"meat", 1.2},
                {"veggies", 0.8},
                {"cheese", 1.1},
                {"sauce", 0.9}
            };
            Name = name;
            Weight = weight;
        }

        public string Name
        {
            get => name;
            set
            {
                if (!toppingType.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                name = value;
            }
        }
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{Name} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }
        public double Calories
        {
            get => BaseCaloriesPerGram * Weight * toppingType[name.ToLower()];
        }
    }
}
