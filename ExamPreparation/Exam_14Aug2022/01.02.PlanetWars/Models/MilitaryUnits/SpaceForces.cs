namespace PlanetWars.Models.MilitaryUnits
{
    public class SpaceForces : MilitaryUnit
    {
        private const double CostSpaceForces = 11d;

        public SpaceForces()
            : base(CostSpaceForces)
        {
        }
    }
}
