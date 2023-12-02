using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorations;
        private readonly List<IAquarium> aquariums;
        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != nameof(FreshwaterAquarium) && aquariumType != nameof(SaltwaterAquarium))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            IAquarium aquarium;
            if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }

            aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType != nameof(Ornament) && decorationType != nameof(Plant))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            IDecoration decoration;
            if (decorationType == nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else
            {
                decoration = new Plant();
            }

            decorations.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorations.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            aquarium.AddDecoration(decoration);
            decorations.Remove(decoration);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType != nameof(FreshwaterFish) && fishType != nameof(SaltwaterFish))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            if ((fishType == "FreshwaterFish" && aquarium.GetType().Name != nameof(FreshwaterAquarium))
                || (fishType == "SaltwaterFish" && aquarium.GetType().Name != nameof(SaltwaterAquarium)))
            {
                return string.Format(OutputMessages.UnsuitableWater);
            }

            IFish fish = null;
            if (fishType == nameof(SaltwaterFish) && aquarium.GetType().Name == nameof(SaltwaterAquarium))
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == nameof(FreshwaterFish) && aquarium.GetType().Name == nameof(FreshwaterAquarium))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            aquarium.AddFish(fish);
            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            decimal value = aquarium.Fish.Sum(a => a.Price) + aquarium.Decorations.Sum(d => d.Price);
            return string.Format(OutputMessages.AquariumValue, aquariumName, value);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var a in aquariums)
            {
                sb.AppendLine(a.GetInfo());
            }
            return sb.ToString().Trim();
        }
    }
}
