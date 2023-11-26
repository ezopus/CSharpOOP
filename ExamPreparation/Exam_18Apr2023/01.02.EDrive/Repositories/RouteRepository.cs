using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private readonly List<IRoute> models;

        public RouteRepository()
        {
            models = new List<IRoute>();
        }
        public void AddModel(IRoute model)
        {
            models.Add(model);
        }

        public bool RemoveById(string identifier)
        {
            return models.Remove(models.FirstOrDefault(m => m.RouteId == int.Parse(identifier)));
        }

        public IRoute FindById(string identifier)
        {
            return models.FirstOrDefault(m => m.RouteId == int.Parse(identifier));
        }

        public IReadOnlyCollection<IRoute> GetAll() => models.AsReadOnly();
    }
}
