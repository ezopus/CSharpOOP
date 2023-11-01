
namespace Restaurant
{
    public class Tea : HotBeverage
    {
        private const double CoffeeMilliliters = 50;
        private const decimal CoffeePrice = 3.50m;

        public Tea(string name, decimal price, double milliliters, double caffeine)
            : base (name, price, milliliters)
        {
            Caffeine = caffeine;
        }
        public double Caffeine { get; set; }
    }
}
