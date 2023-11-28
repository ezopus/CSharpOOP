using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private const int winPoints = 3;
        private const int drawPoints = 1;

        private string name;

        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            players = new List<IPlayer>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }
                name = value;
            }
        }
        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (players.Any())
                {
                    return Math.Round(players.Average(p => p.Rating), 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();
        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            PointsEarned += winPoints;
            foreach (var player in players)
            {
                player.IncreaseRating();
            }
        }

        public void Lose()
        {
            foreach (var player in players)
            {
                player.DecreaseRating();
            }
        }

        public void Draw()
        {
            PointsEarned += drawPoints;
            IPlayer goalKeeper = players.FirstOrDefault(p => p.GetType() == typeof(Goalkeeper));
            goalKeeper.IncreaseRating();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            if (players.Any())
            {
                sb.Append($"--Players: {string.Join(", ", players.Select(p => p.Name))}");
            }
            else
            {
                sb.AppendLine($"--Players: none");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

