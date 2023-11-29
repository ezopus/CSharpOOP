using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;

namespace Formula1.Models.Models
{
    public class Pilot : IPilot
    {
        private string fullname;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            FullName = fullName;
            CanRace = false;
        }

        public string FullName
        {
            get => fullname;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                fullname = value;
            }
        }

        public IFormulaOneCar Car => car;
        public int NumberOfWins { get; private set; }
        public bool CanRace { get; private set; }
        public void AddCar(IFormulaOneCar car)
        {
            CanRace = true;
            this.car = car;
        }
        public void WinRace() => NumberOfWins++;

        public override string ToString()
        {
            return $"Pilot {FullName} has {NumberOfWins} wins.";
        }
    }
}
