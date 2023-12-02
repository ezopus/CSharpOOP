namespace PlanetWars.Models.MilitaryUnits
{
    public class StormTroopers : MilitaryUnit
    {
        private const double CostStormTroopers = 2.5d;

        public StormTroopers()
            : base(CostStormTroopers)
        {
        }
    }
}
