using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> models;
        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => models;
        public void Add(IPlanet model) => models.Add(model);
        public bool Remove(IPlanet model) => models.Remove(model);
        public IPlanet FindByName(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
