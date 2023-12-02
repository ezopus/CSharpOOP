using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            this.cost = cost;
            enduranceLevel = 1;
        }
        public double Cost
        {
            get => cost;
            private set => cost = value;
        }
        public int EnduranceLevel
        {
            get => enduranceLevel;
            private set => enduranceLevel = value;
        }
        public void IncreaseEndurance()
        {
            if (EnduranceLevel + 1 > 20)
            {
                EnduranceLevel = 20;
                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
            }

            EnduranceLevel += 1;
        }
    }
}
