using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Utilities.Messages;
using System.Text;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private PeakRepository peaks;
        private ClimberRepository climbers;
        private BaseCamp baseCamp;

        public Controller()
        {
            peaks = new PeakRepository();
            climbers = new ClimberRepository();
            baseCamp = new BaseCamp();
        }

        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            if (peaks.Get(name) != null)
            {
                return string.Format(OutputMessages.PeakAlreadyAdded, name);
            }

            if (difficultyLevel != "Extreme" && difficultyLevel != "Hard" && difficultyLevel != "Moderate")
            {
                return string.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
            }

            IPeak peak = new Peak(name, elevation, difficultyLevel);
            peaks.Add(peak);
            return string.Format(OutputMessages.PeakIsAllowed, name, peaks.GetType().Name);
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            if (climbers.Get(name) != null)
            {
                return string.Format(OutputMessages.ClimberCannotBeDuplicated, name, climbers.GetType().Name);
            }

            IClimber climber;
            if (isOxygenUsed)
            {
                climber = new OxygenClimber(name);
            }
            else
            {
                climber = new NaturalClimber(name);
            }

            climbers.Add(climber);
            baseCamp.ArriveAtCamp(climber.Name);
            return string.Format(OutputMessages.ClimberArrivedAtBaseCamp, climber.Name);
        }

        public string AttackPeak(string climberName, string peakName)
        {
            IClimber climber = climbers.Get(climberName);
            if (climber == null)
            {
                return string.Format(OutputMessages.ClimberNotArrivedYet, climberName);
            }

            IPeak peak = peaks.Get(peakName);
            if (peak == null)
            {
                return string.Format(OutputMessages.PeakIsNotAllowed, peakName);
            }

            if (!baseCamp.Residents.Contains(climberName))
            {
                return string.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
            }

            if (peak.DifficultyLevel == "Extreme" && climber.GetType() == typeof(NaturalClimber))
            {
                return string.Format(OutputMessages.NotCorrespondingDifficultyLevel, climber.Name, peak.Name);
            }

            baseCamp.LeaveCamp(climber.Name);
            climber.Climb(peak);
            if (climber.Stamina == 0)
            {
                return string.Format(OutputMessages.NotSuccessfullAttack, climber.Name);
            }

            baseCamp.ArriveAtCamp(climber.Name);
            return string.Format(OutputMessages.SuccessfulAttack, climber.Name, peak.Name);

        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            IClimber climber = climbers.Get(climberName);

            if (!baseCamp.Residents.Contains(climberName))
            {
                return string.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
            }

            if (climber.Stamina == 10)
            {
                return string.Format(OutputMessages.NoNeedOfRecovery, climber.Name);
            }

            climber.Rest(daysToRecover);
            return string.Format(OutputMessages.ClimberRecovered, climber.Name, daysToRecover);
        }

        public string BaseCampReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BaseCamp residents:");
            if (baseCamp.Residents.Any())
            {
                foreach (var climber in baseCamp.Residents.OrderBy(x => x))
                {
                    var currentClimber = climbers.Get(climber);
                    sb.AppendLine($"Name: {currentClimber.Name}, Stamina: {currentClimber.Stamina}, Count of Conquered Peaks: {currentClimber.ConqueredPeaks.Count}");
                }
            }
            else
            {
                sb.AppendLine("BaseCamp is currently empty.");
            }

            return sb.ToString().Trim();
        }

        public string OverallStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***Highway-To-Peak***");

            foreach (var climber in climbers.All.OrderByDescending(c => c.ConqueredPeaks.Count).ThenBy(c => c.Name))
            {
                var currentClimberPeaks = new PeakRepository();
                foreach (var peak in peaks.All)
                {
                    if (climber.ConqueredPeaks.Contains(peak.Name))
                    {
                        currentClimberPeaks.Add(peak);
                    }
                }

                sb.AppendLine(climber.ToString());
                foreach (var peak in currentClimberPeaks.All.OrderByDescending(p => p.Elevation))
                {
                    sb.AppendLine(peak.ToString());
                }
            }


            return sb.ToString().Trim();
        }
    }
}
