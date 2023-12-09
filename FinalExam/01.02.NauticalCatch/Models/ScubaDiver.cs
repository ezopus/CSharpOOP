namespace NauticalCatchChallenge.Models
{
    public class ScubaDiver : Diver
    {
        private const int ScubaDiverOxygenLevel = 540;
        private const double ScubaDiverMissDecrease = 0.3d;
        public ScubaDiver(string name)
            : base(name, ScubaDiverOxygenLevel)
        {
        }

        public override void Miss(int TimeToCatch)
        {
            this.OxygenLevel -= (int)Math.Round(ScubaDiverMissDecrease * TimeToCatch, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy()
        {
            if (OxygenLevel < ScubaDiverOxygenLevel)
            {
                OxygenLevel = ScubaDiverOxygenLevel;
            }
        }
    }
}
