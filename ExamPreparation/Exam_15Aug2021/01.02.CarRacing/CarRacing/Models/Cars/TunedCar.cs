namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double TunedCarFuelAvailable = 65;
        private const double TunedCarFuelPerRace = 7.5;
        public TunedCar(string make, string model, string vin, int horsePower)
            : base(make, model, vin, horsePower, TunedCarFuelAvailable, TunedCarFuelPerRace)
        {
        }
    }
}
