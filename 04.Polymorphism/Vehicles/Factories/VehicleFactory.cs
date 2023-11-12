using Vehicles.Factories.Interfaces;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle Create(string type, double quantity, double consumption, double tankCapacity)
        {
            switch (type)
            {
                case "Car":
                    return new Car(quantity, consumption, tankCapacity);
                case "Truck":
                    return new Truck(quantity, consumption, tankCapacity);
                case "Bus":
                    return new Bus(quantity, consumption, tankCapacity);
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
        }

    }
}
