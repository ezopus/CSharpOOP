﻿using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Utilities.Messages;
using System.Text;

namespace HighwayToPeak.Models
{
    public abstract class Climber : IClimber
    {
        private string name;
        private int stamina;
        private List<string> conqueredPeaks;

        protected Climber(string name, int stamina)
        {
            this.name = name;
            this.stamina = stamina;
            conqueredPeaks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClimberNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Stamina
        {
            get => stamina;
            protected set
            {
                stamina = value;
                if (stamina < 0)
                {
                    stamina = 0;
                }
                if (stamina > 10)
                {
                    stamina = 10;
                }
            }
        }
        public IReadOnlyCollection<string> ConqueredPeaks => conqueredPeaks;
        public void Climb(IPeak peak)
        {
            if (!conqueredPeaks.Contains(peak.Name))
            {
                conqueredPeaks.Add(peak.Name);
            }
            if (peak.DifficultyLevel == "Extreme")
            {
                Stamina -= 6;
            }
            else if (peak.DifficultyLevel == "Hard")
            {
                Stamina -= 4;
            }
            else
            {
                Stamina -= 2;
            }
        }

        public abstract void Rest(int daysCount);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name} - Name: {Name}, Stamina: {Stamina}");
            if (conqueredPeaks.Count > 0)
            {
                sb.AppendLine($"Peaks conquered: {conqueredPeaks.Count}");
            }
            else
            {
                sb.AppendLine($"Peaks conquered: no peaks conquered");
            }
            return sb.ToString().Trim();
        }
    }
}
