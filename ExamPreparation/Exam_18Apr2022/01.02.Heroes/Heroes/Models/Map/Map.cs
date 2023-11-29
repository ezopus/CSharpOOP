using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        private readonly List<IHero> knights;
        private readonly List<IHero> barbarians;
        public Map()
        {
            knights = new List<IHero>();
            barbarians = new List<IHero>();
        }
        public string Fight(ICollection<IHero> players)
        {
            foreach (var hero in players.Where(p => p.Weapon != null && p.IsAlive))
            {
                if (hero.GetType().Name == nameof(Barbarian))
                {
                    barbarians.Add(hero);
                }
                else
                {
                    knights.Add(hero);
                }
            }

            while (knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                foreach (var knight in knights.Where(k => k.IsAlive))
                {
                    foreach (var barbarian in barbarians.Where(b => b.IsAlive))
                    {
                        barbarian.TakeDamage(knight.Weapon.DoDamage());
                    }
                }

                foreach (var barbarian in barbarians.Where(b => b.IsAlive))
                {
                    foreach (var knight in knights.Where(k => k.IsAlive))
                    {
                        knight.TakeDamage(barbarian.Weapon.DoDamage());
                    }
                }
            }

            if (knights.Any(k => k.IsAlive) && !barbarians.Any(b => b.IsAlive))
            {
                return string.Format(OutputMessages.MapFightKnightsWin, knights.Count(k => !k.IsAlive));
            }

            if (!knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                return string.Format(OutputMessages.MapFigthBarbariansWin, barbarians.Count(b => !b.IsAlive));
            }

            return default;
        }
    }
}
