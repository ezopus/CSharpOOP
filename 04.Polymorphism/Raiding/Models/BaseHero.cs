using Raiding.Models.Interfaces;

namespace Raiding.Models
{
    public abstract class BaseHero : IBaseHero
    {
        protected BaseHero(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public string Name { get; }
        public int Power { get; }
        public virtual string CastAbility()
        {
            throw new NotImplementedException();
        }
    }
}
