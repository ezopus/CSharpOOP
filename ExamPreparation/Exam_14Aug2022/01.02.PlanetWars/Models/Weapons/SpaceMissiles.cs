namespace PlanetWars.Models.Weapons
{
    public class SpaceMissiles : Weapon
    {
        private const double PriceSpaceMissiles = 8.75d;
        public SpaceMissiles(int destructionLevel)
            : base(destructionLevel, PriceSpaceMissiles)
        {
        }
    }
}
