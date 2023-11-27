using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;

        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models
        {
            get => models.AsReadOnly();
        }
        public void AddItem(IPlanet model)
        {
            models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return models.FirstOrDefault(m => m.Name == name);
        }

        public bool RemoveItem(string name)
        {
            return models.Remove(models.FirstOrDefault(m => m.Name == name));
        }
    }
}
