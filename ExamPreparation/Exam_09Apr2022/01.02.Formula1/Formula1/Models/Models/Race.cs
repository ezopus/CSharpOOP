using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private readonly List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            TookPlace = false;
            pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get => raceName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                raceName = value;
            }

        }
        public int NumberOfLaps
        {
            get => numberOfLaps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                numberOfLaps = value;
            }
        }
        public bool TookPlace { get; set; }
        public ICollection<IPilot> Pilots => pilots.AsReadOnly();
        public void AddPilot(IPilot pilot) => pilots.Add(pilot);
        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {RaceName} race has:");
            sb.AppendLine($"Participants: {pilots.Count}");
            sb.AppendLine($"Number of laps: {NumberOfLaps}");
            sb.Append($"Took place: ");
            if (TookPlace)
            {
                sb.AppendLine("Yes");
            }
            else
            {
                sb.AppendLine("No");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
