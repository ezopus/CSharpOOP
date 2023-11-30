namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double BiologistOxygen = 70;
        private const double BiologistDecreaseOxygen = 5;
        public Biologist(string name)
            : base(name, BiologistOxygen)
        {
        }

        public override void Breath()
        {
            Oxygen -= BiologistDecreaseOxygen;
        }
    }
}
