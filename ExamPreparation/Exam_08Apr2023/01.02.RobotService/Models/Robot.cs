using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private List<int> interfaceStandards;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            Model = model;
            BatteryCapacity = batteryCapacity;
            BatteryLevel = batteryCapacity;
            ConvertionCapacityIndex = conversionCapacityIndex;
            this.interfaceStandards = new List<int>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNullOrWhitespace);
                }
                model = value;
            }
        }

        public int BatteryCapacity
        {
            get => batteryCapacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.BatteryCapacityBelowZero);
                }
                batteryCapacity = value;
            }
        }
        public int BatteryLevel { get; private set; }
        public int ConvertionCapacityIndex { get; private set; }
        public IReadOnlyCollection<int> InterfaceStandards { get => interfaceStandards.AsReadOnly(); }
        public void Eating(int minutes)
        {
            for (int i = 1; i <= minutes; i++)
            {
                if (this.BatteryLevel >= this.BatteryCapacity)
                {
                    this.BatteryLevel = BatteryCapacity;
                    break;
                }

                BatteryLevel += ConvertionCapacityIndex;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            interfaceStandards.Add(supplement.InterfaceStandard);
            BatteryCapacity -= supplement.BatteryUsage;
            BatteryLevel -= supplement.BatteryUsage;
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (BatteryLevel >= consumedEnergy)
            {
                BatteryLevel -= consumedEnergy;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name} {Model}");
            sb.AppendLine($"--Maximum battery capacity: {BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {BatteryLevel}");
            if (InterfaceStandards.Any())
            {
                sb.AppendLine($"--Supplements installed: {string.Join(" ", InterfaceStandards)}");
            }
            else
            {
                sb.AppendLine("--Supplements installed: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
