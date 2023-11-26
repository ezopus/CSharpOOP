namespace EDriveRent.Models
{
    public class CargoVan : Vehicle
    {
        private const double CargoVanMaxMileage = 180;
        public CargoVan(string brand, string model, string licensePlate)
            : base(brand, model, CargoVanMaxMileage, licensePlate)
        {
        }
    }
}
