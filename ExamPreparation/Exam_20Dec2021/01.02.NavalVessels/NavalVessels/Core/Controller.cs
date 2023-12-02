using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private readonly List<ICaptain> captainList;
        public Controller()
        {
            vessels = new VesselRepository();
            captainList = new List<ICaptain>();
        }
        public string HireCaptain(string fullName)
        {
            if (captainList.Any(c => c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }

            ICaptain captain = new Captain(fullName);
            captainList.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vesselType != nameof(Battleship) && vesselType != nameof(Submarine))
            {
                return string.Format(OutputMessages.InvalidVesselType);
            }

            if (vessels.FindByName(name) != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }

            IVessel vessel;
            if (vesselType == nameof(Battleship))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            if (!captainList.Any(c => c.FullName == selectedCaptainName))
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            if (vessels.FindByName(selectedVesselName) == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            IVessel currentVessel = vessels.FindByName(selectedVesselName);

            if (currentVessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            ICaptain captain = captainList.FirstOrDefault(c => c.FullName == selectedCaptainName);

            currentVessel.Captain = captain;
            captain.AddVessel(currentVessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = captainList.FirstOrDefault(c => c.FullName == captainFullName);
            return captain.Report();
        }

        public string VesselReport(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            return vessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            if (vessel is Battleship)
            {
                Battleship battleship = vessel as Battleship;
                battleship.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, battleship.Name);
            }
            else
            {
                Submarine submarine = vessel as Submarine;
                submarine.ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, submarine.Name);
            }
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attacker = vessels.FindByName(attackingVesselName);
            IVessel defender = vessels.FindByName(defendingVesselName);
            if (attacker == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }

            if (defender == null)
            {
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if (attacker.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attacker.Name);
            }

            if (defender.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defender.Name);
            }

            attacker.Attack(defender);
            attacker.Captain.IncreaseCombatExperience();
            defender.Captain.IncreaseCombatExperience();

            return string.Format(OutputMessages.SuccessfullyAttackVessel, defender.Name, attacker.Name, defender.ArmorThickness);
        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vesselToRepair = vessels.FindByName(vesselName);
            if (vesselToRepair == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            vesselToRepair.RepairVessel();
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselToRepair.Name);
        }
    }
}
