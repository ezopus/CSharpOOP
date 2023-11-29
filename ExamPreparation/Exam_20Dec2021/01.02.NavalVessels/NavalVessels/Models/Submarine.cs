using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel
    {
        private const double SubmarineArmorThickness = 200;
        public Submarine(string name, double mainWeaponCaliber, double speed)
            : base(name, SubmarineArmorThickness, mainWeaponCaliber, speed)
        {
            SubmergeMode = false;
        }
        public bool SubmergeMode { get; private set; }

        public void ToggleSubmergeMode()
        {
            if (!SubmergeMode)
            {
                SubmergeMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            else
            {
                SubmergeMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < SubmarineArmorThickness)
            {
                this.ArmorThickness = SubmarineArmorThickness;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.Append($" *Submerge mode: ");
            if (SubmergeMode)
            {
                sb.AppendLine("ON");
            }
            else
            {
                sb.AppendLine("OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
