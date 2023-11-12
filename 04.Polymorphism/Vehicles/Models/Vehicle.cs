using Vehicles.Models.Interfaces;
using ArgumentException = System.ArgumentException;

namespace Vehicles.Models
{
    public class Vehicle : IVehicle
    {
        private double fuelQuantity;
        private double increasedConsumption;
        public Vehicle(double fuelQuantity, double fuelConsumption, double increasedConsumption, double tankCapacity)
        {
            TankCapacity = tankCapacity;
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            this.increasedConsumption = increasedConsumption;
        }
        public double FuelConsumption { get; private set; }
        public double FuelQuantity
        {
            get => fuelQuantity;
            private set
            {
                if (TankCapacity < value)
                {
                    fuelQuantity = 0;
                }
                else
                {
                    fuelQuantity = value;
                }
            }
        }
        public double TankCapacity { get; private set; }

        public string Drive(double distance, bool hasIncreasedConsumption = true)
        {
            double consumption = FuelConsumption;

            if (hasIncreasedConsumption)
            {
                consumption += increasedConsumption;
            }

            if (FuelQuantity < consumption * distance)
            {
                throw new ArgumentException($"{this.GetType().Name} needs refueling");
            }

            FuelQuantity -= consumption * distance;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (FuelQuantity + amount > TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {amount} fuel in the tank");
            }

            FuelQuantity += amount;
        }

        public override string ToString()
            => $"{this.GetType().Name}: {FuelQuantity:f2}";
    }
}
