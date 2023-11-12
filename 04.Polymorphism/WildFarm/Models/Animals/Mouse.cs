using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        public const double MouseWeightMultiplier = 0.1;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }
        protected override IReadOnlyCollection<Type> FoodType
            => new HashSet<Type> { typeof(Vegetable), typeof(Fruit) };
        protected override double WeightMultiplier
            => MouseWeightMultiplier;
        public override string ProduceSound()
            => "Squeak";
        public override string ToString()
            => base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
