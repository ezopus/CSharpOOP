using Easter.Models.Bunnies.Contracts;
using Easter.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Easter.Repositories
{
    public class BunnyRepository : IRepository<IBunny>
    {
        private readonly List<IBunny> models;
        public BunnyRepository()
        {
            models = new List<IBunny>();
        }
        public IReadOnlyCollection<IBunny> Models => models;
        public void Add(IBunny model) => models.Add(model);
        public bool Remove(IBunny model) => models.Remove(model);
        public IBunny FindByName(string name) => models.FirstOrDefault(b => b.Name == name);
    }
}
