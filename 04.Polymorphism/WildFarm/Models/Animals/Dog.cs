using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double DogWeightMultiplier = 0.4;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }
        protected override IReadOnlyCollection<Type> FoodType
            => new HashSet<Type> { typeof(Meat) };
        protected override double WeightMultiplier
            => DogWeightMultiplier;
        public override string ProduceSound()
            => "Woof!";
        public override string ToString()
            => base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";

    }
}
