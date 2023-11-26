namespace EDriveRent.Models
{
    public class PassengerCar : Vehicle
    {
        private const double CargoVanMaxMileage = 450;
        public PassengerCar(string brand, string model, string licensePlate)
            : base(brand, model, CargoVanMaxMileage, licensePlate)
        {
        }
    }
}
