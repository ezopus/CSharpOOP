using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double HenMultiplier = 0.35;
        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }
        protected override IReadOnlyCollection<Type> FoodType
            => new HashSet<Type> { typeof(Vegetable), typeof(Fruit), typeof(Meat), typeof(Seeds) };
        protected override double WeightMultiplier
            => HenMultiplier;
        public override string ProduceSound()
            => "Cluck";

    }
}
