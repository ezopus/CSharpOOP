using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> models;
        public HeroRepository()
        {
            models = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => models;
        public void Add(IHero model) => models.Add(model);
        public bool Remove(IHero model) => models.Remove(models.FirstOrDefault(m => m.Name == model.Name));
        public IHero FindByName(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
