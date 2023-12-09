using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Models.FishModels;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Utilities.Messages;
using System.Text;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private DiverRepository divers;
        private FishRepository fish;

        public Controller()
        {
            divers = new DiverRepository();
            fish = new FishRepository();
        }

        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                return string.Format(OutputMessages.DiverTypeNotPresented, diverType);
            }

            if (divers.GetModel(diverName) != null)
            {
                return string.Format(OutputMessages.DiverNameDuplication, diverName, divers.GetType().Name);
            }

            IDiver diver;
            if (diverType == nameof(FreeDiver))
            {
                diver = new FreeDiver(diverName);
            }
            else
            {
                diver = new ScubaDiver(diverName);
            }

            divers.AddModel(diver);
            return string.Format(OutputMessages.DiverRegistered, diverName, divers.GetType().Name);

        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != nameof(DeepSeaFish) && fishType != nameof(PredatoryFish) && fishType != nameof(ReefFish))
            {
                return string.Format(OutputMessages.FishTypeNotPresented, fishType);
            }

            if (fish.GetModel(fishName) != null)
            {
                return string.Format(OutputMessages.FishNameDuplication, fishName, fish.GetType().Name);
            }

            IFish currentFish;
            if (fishType == nameof(DeepSeaFish))
            {
                currentFish = new DeepSeaFish(fishName, points);
            }
            else if (fishType == nameof(PredatoryFish))
            {
                currentFish = new PredatoryFish(fishName, points);
            }
            else
            {
                currentFish = new ReefFish(fishName, points);
            }

            fish.AddModel(currentFish);
            return string.Format(OutputMessages.FishCreated, fishName);
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver currentDiver = divers.GetModel(diverName);
            if (currentDiver == null)
            {
                return string.Format(OutputMessages.DiverNotFound, divers.GetType().Name, diverName);
            }

            IFish currentFish = fish.GetModel(fishName);
            if (currentFish == null)
            {
                return string.Format(OutputMessages.FishNotAllowed, fishName);
            }

            if (currentDiver.HasHealthIssues)
            {
                return string.Format(OutputMessages.DiverHealthCheck, currentDiver.Name);
            }

            if (currentDiver.OxygenLevel < currentFish.TimeToCatch)
            {
                currentDiver.Miss(currentFish.TimeToCatch);
                if (currentDiver.OxygenLevel == 0)
                {
                    currentDiver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, currentDiver.Name, currentFish.Name);
            }

            if (currentDiver.OxygenLevel == currentFish.TimeToCatch)
            {
                if (isLucky)
                {
                    currentDiver.Hit(currentFish);
                    if (currentDiver.OxygenLevel == 0)
                    {
                        currentDiver.UpdateHealthStatus();
                    }
                    return string.Format(OutputMessages.DiverHitsFish, currentDiver.Name, currentFish.Points, currentFish.Name);
                }

                currentDiver.Miss(currentFish.TimeToCatch);
                if (currentDiver.OxygenLevel == 0)
                {
                    currentDiver.UpdateHealthStatus();
                }

                return string.Format(OutputMessages.DiverMisses, currentDiver.Name, currentFish.Name);
            }

            currentDiver.Hit(currentFish);
            if (currentDiver.OxygenLevel == 0)
            {
                currentDiver.UpdateHealthStatus();
            }

            return string.Format(OutputMessages.DiverHitsFish, currentDiver.Name, currentFish.Points, currentFish.Name);
        }

        public string HealthRecovery()
        {
            int recoveredDivers = 0;
            foreach (var diver in divers.Models.Where(d => d.HasHealthIssues))
            {
                diver.UpdateHealthStatus();
                diver.RenewOxy();
                recoveredDivers++;
            }

            return string.Format(OutputMessages.DiversRecovered, recoveredDivers);
        }

        public string DiverCatchReport(string diverName)
        {
            StringBuilder sb = new StringBuilder();
            IDiver currentDiver = divers.GetModel(diverName);

            sb.AppendLine(currentDiver.ToString());

            sb.AppendLine("Catch Report:");
            foreach (var fishCaught in currentDiver.Catch)
            {
                sb.AppendLine(fish.GetModel(fishCaught).ToString());
            }

            return sb.ToString().Trim();
        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("**Nautical-Catch-Challenge**");
            foreach (var diver in divers.Models
                             .OrderByDescending(d => d.CompetitionPoints)
                             .ThenByDescending(d => d.Catch.Count)
                             .ThenBy(d => d.Name)
                             .Where(d => d.HasHealthIssues == false))
            {
                sb.AppendLine(diver.ToString());
            }


            return sb.ToString().Trim();
        }
    }
}
