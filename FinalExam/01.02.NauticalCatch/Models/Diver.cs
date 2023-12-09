using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Utilities.Messages;

namespace NauticalCatchChallenge.Models
{
    public abstract class Diver : IDiver
    {
        private string name;
        private int oxygenLevel;
        private readonly List<string> _catch;
        private double competitionPoints;
        protected Diver(string name, int oxygenLevel)
        {
            Name = name;
            OxygenLevel = oxygenLevel;
            _catch = new List<string>();
            CompetitionPoints = 0;
            HasHealthIssues = false;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DiversNameNull);
                }
                name = value;
            }
        }

        public int OxygenLevel
        {
            get => oxygenLevel;
            protected set
            {
                if (value < 0)
                {
                    oxygenLevel = 0;
                }
                else
                {
                    oxygenLevel = value;
                }
            }
        }

        public IReadOnlyCollection<string> Catch => _catch;
        public double CompetitionPoints
        {
            get => competitionPoints;
            private set => competitionPoints = value;

        }
        public bool HasHealthIssues { get; private set; }
        public void Hit(IFish fish)
        {
            OxygenLevel -= fish.TimeToCatch;
            _catch.Add(fish.Name);
            CompetitionPoints += fish.Points;
        }

        public abstract void Miss(int timeToCatch);
        public abstract void RenewOxy();
        public void UpdateHealthStatus()
        {
            if (HasHealthIssues)
            {
                HasHealthIssues = false;
            }
            else
            {
                HasHealthIssues = true;
            }
        }

        public override string ToString()
        {
            if (CompetitionPoints > 0)
            {
                return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {_catch.Count}, Points earned: {CompetitionPoints:f1} ]";
            }
            return $"Diver [ Name: {Name}, Oxygen left: {OxygenLevel}, Fish caught: {_catch.Count}, Points earned: 0 ]";
        }

    }
}
