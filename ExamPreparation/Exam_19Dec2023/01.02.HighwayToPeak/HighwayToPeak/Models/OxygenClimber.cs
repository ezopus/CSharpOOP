namespace HighwayToPeak.Models
{
    public class OxygenClimber : Climber
    {
        private const int OxygenClimberStamina = 10;
        public OxygenClimber(string name)
            : base(name, OxygenClimberStamina)
        {
        }

        public override void Rest(int daysCount)
        {
            for (int i = 0; i < daysCount; i++)
            {
                this.Stamina += 1;
            }
        }
    }
}
