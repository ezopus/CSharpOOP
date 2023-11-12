

namespace Vehicles.Models.Interfaces
{
    public interface IVehicle
    {
        double FuelConsumption { get; }
        double FuelQuantity { get; }
        string Drive(double distance, bool isIncreased = true);
        void Refuel(double amount);

    }
}
