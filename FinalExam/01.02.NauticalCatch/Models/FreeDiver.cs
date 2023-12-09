namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int FreeDiverOxygenLevel = 120;
        private const double FreeDiverMissDecrease = 0.6d;
        public FreeDiver(string name)
            : base(name, FreeDiverOxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            this.OxygenLevel -= (int)Math.Round(FreeDiverMissDecrease * TimeToCatch, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy()
        {
            if (OxygenLevel < FreeDiverOxygenLevel)
            {
                OxygenLevel = FreeDiverOxygenLevel;
            }
        }
    }
}
