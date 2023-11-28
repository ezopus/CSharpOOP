using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> models;

        public RaceRepository()
        {
            models = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => models;
        public void Add(IRace model) => models.Add(model);

        public bool Remove(IRace model) => models.Remove(model);

        public IRace FindByName(string name) => models.FirstOrDefault(m => m.RaceName == name);
    }
}
