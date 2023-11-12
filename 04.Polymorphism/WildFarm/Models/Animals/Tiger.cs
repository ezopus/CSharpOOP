using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        private const double TigerWeightMultiplier = 1.0;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }
        protected override IReadOnlyCollection<Type> FoodType
            => new HashSet<Type> { typeof(Meat) };
        protected override double WeightMultiplier
            => TigerWeightMultiplier;
        public override string ProduceSound()
            => "ROAR!!!";
    }
}
