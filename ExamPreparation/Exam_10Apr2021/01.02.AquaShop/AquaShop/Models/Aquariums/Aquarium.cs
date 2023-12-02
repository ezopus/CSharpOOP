using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private readonly List<IDecoration> decorations;
        private readonly List<IFish> _fish;

        protected Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            decorations = new List<IDecoration>();
            _fish = new List<IFish>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                name = value;
            }
        }
        public int Capacity { get; private set; }
        public int Comfort => Decorations.Sum(d => d.Comfort);
        public ICollection<IDecoration> Decorations => decorations;
        public ICollection<IFish> Fish => _fish;
        public void AddFish(IFish fish)
        {
            if (_fish.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            _fish.Add(fish);
        }

        public bool RemoveFish(IFish fish) => _fish.Remove(fish);
        public void AddDecoration(IDecoration decoration) => this.decorations.Add(decoration);

        public void Feed()
        {
            foreach (var f in Fish)
            {
                f.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({GetType().Name}):");
            sb.Append($"Fish: ");
            if (Fish.Any())
            {
                sb.AppendLine(string.Join(", ", Fish.Select(f => f.Name)));
            }
            else
            {
                sb.AppendLine("none");
            }

            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");

            return sb.ToString().Trim();
        }
    }
}
