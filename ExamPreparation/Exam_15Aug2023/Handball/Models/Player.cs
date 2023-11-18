using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;

namespace Handball.Models
{
    public abstract class Player : IPlayer
    {
        private string name;
        private string team;
        protected Player(string name, double rating)
        {
            Name = name;
            Rating = rating;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.PlayerNameNull);
                }
                name = value;
            }
        }

        public double Rating { get; protected set; }
        public string Team
        {
            get => team;
            private set => team = value;
        }
        public void JoinTeam(string name)
        {
            Team = name;
        }

        public abstract void IncreaseRating();

        public abstract void DecreaseRating();

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Name}" + Environment.NewLine + $"--Rating: {Rating}";
        }
    }
}
