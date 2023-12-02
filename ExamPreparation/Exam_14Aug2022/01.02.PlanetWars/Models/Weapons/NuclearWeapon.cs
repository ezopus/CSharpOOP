namespace PlanetWars.Models.Weapons
{
    public class NuclearWeapon : Weapon
    {
        private const double PriceNuclearWeapon = 15d;
        public NuclearWeapon(int destructionLevel)
            : base(destructionLevel, PriceNuclearWeapon)
        {
        }
    }
}
