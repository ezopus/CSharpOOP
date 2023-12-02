using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private readonly List<string> targets;
        private double defaultArmorThickness;

        public Vessel(string name, double armorThickness, double mainWeaponCaliber, double speed)
        {
            Name = name;
            ArmorThickness = armorThickness;
            defaultArmorThickness = armorThickness;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            targets = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.InvalidVesselName));
                }
                name = value;
            }
        }
        public ICaptain Captain
        {
            get => captain;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
                }
                captain = value;
            }

        }
        public double ArmorThickness { get; set; }
        public double MainWeaponCaliber { get; protected set; }
        public double Speed { get; protected set; }
        public ICollection<string> Targets => targets.AsReadOnly();
        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }

            target.ArmorThickness -= this.MainWeaponCaliber;
            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }
            targets.Add(target.Name);
        }

        public abstract void RepairVessel();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {Speed} knots");
            sb.Append(" *Targets: ");
            if (targets.Any())
            {
                sb.AppendLine(string.Join(", ", targets));
            }
            else
            {
                sb.AppendLine("None");
            }

            return sb.ToString().Trim();
        }
    }
}
