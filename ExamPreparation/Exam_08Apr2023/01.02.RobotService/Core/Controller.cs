using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly SupplementRepository supplements;
        private readonly RobotRepository robots;
        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();
        }
        public string CreateRobot(string model, string typeName)
        {
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            if (typeName == "DomesticAssistant")
            {
                robots.AddNew(new DomesticAssistant(model));
            }
            else
            {
                robots.AddNew(new IndustrialAssistant(model));
            }

            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != nameof(SpecializedArm) && typeName != nameof(LaserRadar))
            {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            if (typeName == nameof(SpecializedArm))
            {
                supplements.AddNew(new SpecializedArm());
            }
            else
            {
                supplements.AddNew(new LaserRadar());
            }

            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement currentSupplement;
            if (supplementTypeName == nameof(SpecializedArm))
            {
                currentSupplement = new SpecializedArm();
            }
            else
            {
                currentSupplement = new LaserRadar();
            }

            ISupplement firstSupplement = supplements.FindByStandard(currentSupplement.InterfaceStandard);

            List<IRobot> robotsNotSupportingCurrentSupplement = robots
                .Models()
                .Where(r => !r.InterfaceStandards.Contains(firstSupplement.InterfaceStandard)
                            && r.Model == model)
                .ToList();

            if (robotsNotSupportingCurrentSupplement.Count == 0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            robotsNotSupportingCurrentSupplement.First().InstallSupplement(firstSupplement);

            supplements.RemoveByName(supplementTypeName);

            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }
        public string PerformService(string serviceName, int interfaceStandard, int totalPowerNeeded)
        {

            List<IRobot> supportedRobots = robots.Models().Where(r => r.InterfaceStandards.Contains(interfaceStandard)).ToList();

            if (supportedRobots.Count == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, interfaceStandard);
            }

            supportedRobots = supportedRobots.OrderByDescending(b => b.BatteryLevel).ToList();

            int sumOfBatteryLevel = supportedRobots.Sum(b => b.BatteryLevel);

            if (sumOfBatteryLevel < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, $"{totalPowerNeeded - sumOfBatteryLevel}");
            }
            else
            {
                int robotsWorking = 0;
                while (totalPowerNeeded >= 0)
                {
                    robotsWorking++;
                    IRobot currentRobot = supportedRobots.FirstOrDefault(cr => cr.BatteryLevel > 0);
                    if (currentRobot.BatteryLevel >= totalPowerNeeded)
                    {
                        currentRobot.ExecuteService(totalPowerNeeded);
                        break;
                    }
                    else
                    {
                        totalPowerNeeded -= currentRobot.BatteryLevel;
                        currentRobot.ExecuteService(currentRobot.BatteryLevel);
                    }
                }
                return string.Format(OutputMessages.PerformedSuccessfully, serviceName, robotsWorking);
            }
        }

        public string RobotRecovery(string model, int minutes)
        {
            int eatingRobots = 0;
            foreach (var robot in robots.Models().Where(r => r.BatteryLevel < 50 && r.Model == model))
            {
                robot.Eating(minutes);
                eatingRobots++;
            }

            return string.Format(OutputMessages.RobotsFed, eatingRobots);
        }


        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var robot in robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
