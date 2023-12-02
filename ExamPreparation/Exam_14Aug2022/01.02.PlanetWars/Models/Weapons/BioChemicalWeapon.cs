namespace PlanetWars.Models.Weapons
{
    public class BioChemicalWeapon : Weapon
    {
        private const double PriceBioWeapon = 3.2d;
        public BioChemicalWeapon(int destructionLevel)
            : base(destructionLevel, PriceBioWeapon)
        {
        }
    }
}
