using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double CatWeightMultiplier = 0.3;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }
        protected override IReadOnlyCollection<Type> FoodType
            => new HashSet<Type> { typeof(Vegetable), typeof(Meat) };
        protected override double WeightMultiplier
            => CatWeightMultiplier;
        public override string ProduceSound()
            => "Meow";
    }
}
