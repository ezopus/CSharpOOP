using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;

namespace NauticalCatchChallenge.Repositories
{
    public class DiverRepository : IRepository<IDiver>
    {
        private readonly List<IDiver> models;
        public DiverRepository()
        {
            models = new List<IDiver>();
        }
        public IReadOnlyCollection<IDiver> Models => models;
        public void AddModel(IDiver model) => models.Add(model);
        public IDiver GetModel(string name) => models.FirstOrDefault(m => m.Name == name);
    }
}
