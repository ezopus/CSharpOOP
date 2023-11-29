using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private readonly List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != nameof(BoxingGym) && gymType != nameof(WeightliftingGym))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            IGym gym;
            if (gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
            }
            else
            {
                gym = new WeightliftingGym(gymName);
            }

            gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != nameof(Kettlebell) && equipmentType != nameof(BoxingGloves))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            IEquipment currentEquipment;
            if (equipmentType == nameof(BoxingGloves))
            {
                currentEquipment = new BoxingGloves();
            }
            else
            {
                currentEquipment = new Kettlebell();
            }

            equipment.Add(currentEquipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment currentEquipment = equipment.FindByType(equipmentType);
            if (currentEquipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            IGym currentGym = gyms.FirstOrDefault(g => g.Name == gymName);
            currentGym.AddEquipment(currentEquipment);
            equipment.Remove(currentEquipment);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, currentGym.Name);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if (athleteType != nameof(Boxer) && athleteType != nameof(Weightlifter))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            IGym currentGym = gyms.FirstOrDefault(g => g.Name == gymName);
            if ((athleteType == nameof(Boxer) && currentGym.GetType().Name == nameof(WeightliftingGym))
               || (athleteType == nameof(Weightlifter) && currentGym.GetType().Name == nameof(BoxingGym)))
            {
                return string.Format(OutputMessages.InappropriateGym);
            }

            IAthlete currentAthlete;
            if (athleteType == nameof(Boxer))
            {
                currentAthlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else
            {
                currentAthlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            currentGym.AddAthlete(currentAthlete);
            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            IGym currentGym = gyms.FirstOrDefault(g => g.Name == gymName);
            int trainedAthletes = 0;
            foreach (var athlete in currentGym.Athletes)
            {
                athlete.Exercise();
                trainedAthletes++;
            }

            return string.Format(OutputMessages.AthleteExercise, trainedAthletes);
        }

        public string EquipmentWeight(string gymName)
        {
            IGym currentGym = gyms.FirstOrDefault(g => g.Name == gymName);
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, $"{currentGym.EquipmentWeight:f2}");
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
