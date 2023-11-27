namespace FacadePattern
{
    public class CarAddressBuilder : CarInfoBuilder
    {
        public CarAddressBuilder(Car car)
            : base(car)
        {
            Car = car;
        }

        public CarAddressBuilder InCity(string city)
        {
            Car.City = city;
            return this;
        }

        public CarAddressBuilder AtAddress(string address)
        {
            Car.Address = address;
            return this;
        }
    }
}
