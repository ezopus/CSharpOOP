

namespace PizzaCalories.Models
{
    public class Dough
    {
        private const double BaseCaloriesPerGram = 2;

        private Dictionary<string, double> flourType;
        private Dictionary<string, double> bakingTechnique;
        private double weight;
        private string flourMod;
        private string bakingMod;
        public Dough(string doughType, string technique, double weight)
        {
            flourType = new Dictionary<string, double>
            {
                {"white", 1.5},
                {"wholegrain", 1.0}
            };

            bakingTechnique = new Dictionary<string, double>
            {
                {"crispy", 0.9},
                {"chewy", 1.1},
                {"homemade", 1.0}
            };
            FlourMod = doughType;
            BakingMod = technique;
            Weight = weight;
        }

        public string FlourMod
        {
            get => flourMod;
            set
            {
                if (!flourType.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourMod = value;
            }
        }

        public string BakingMod
        {
            get => bakingMod;
            set
            {
                if (!bakingTechnique.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingMod = value;
            }
        }
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }

        }

        public double Calories
        {
            get => BaseCaloriesPerGram * Weight * flourType[FlourMod.ToLower()] * bakingTechnique [BakingMod.ToLower()];
        }
    }
}
