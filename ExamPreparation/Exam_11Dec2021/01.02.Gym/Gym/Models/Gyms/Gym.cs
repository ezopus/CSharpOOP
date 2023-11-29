using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private readonly List<IAthlete> athletes;
        private readonly List<IEquipment> equipment;
        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            athletes = new List<IAthlete>();
            equipment = new List<IEquipment>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }
                name = value;
            }
        }
        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }
        public double EquipmentWeight => equipment.Sum(e => e.Weight);
        public ICollection<IEquipment> Equipment => equipment;
        public ICollection<IAthlete> Athletes => athletes;
        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count < Capacity)
            {
                athletes.Add(athlete);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }
        }
        public bool RemoveAthlete(IAthlete athlete) => athletes.Remove(athlete);
        public void AddEquipment(IEquipment equipment) => this.equipment.Add(equipment);
        public void Exercise()
        {
            foreach (var athlete in athletes)
            {
                athlete.Exercise();
            }
        }
        public string GymInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} is a {this.GetType().Name}:");
            sb.Append($"Athletes: ");
            if (Athletes.Any())
            {
                sb.AppendLine(string.Join(", ", Athletes.Select(a => a.FullName)));
            }
            else
            {
                sb.AppendLine("No athletes");
            }

            sb.AppendLine($"Equipment total count: {Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {Equipment.Sum(e => e.Weight):f2} grams");

            return sb.ToString().TrimEnd();
        }
    }
}
