namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double IncreasedFuelConsumption = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, IncreasedFuelConsumption, tankCapacity)
        {
        }

        public override void Refuel(double amount)
        {
            if (FuelQuantity + amount > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
            }

            base.Refuel(amount * 0.95);
        }

    }
}
