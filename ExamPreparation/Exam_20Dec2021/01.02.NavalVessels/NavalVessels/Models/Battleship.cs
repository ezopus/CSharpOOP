using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel
    {
        private const double BattleshipArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, BattleshipArmorThickness, mainWeaponCaliber, speed)
        {
            SonarMode = false;
        }
        public bool SonarMode { get; private set; }

        public void ToggleSonarMode()
        {
            if (!SonarMode)
            {
                SonarMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }
            else
            {
                SonarMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }
        }
        public override void RepairVessel()
        {
            if (this.ArmorThickness < BattleshipArmorThickness)
            {
                this.ArmorThickness = BattleshipArmorThickness;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.Append($" *Sonar mode: ");
            if (SonarMode)
            {
                sb.AppendLine("ON");
            }
            else
            {
                sb.AppendLine("OFF");
            }

            return sb.ToString().Trim();
        }
    }
}
