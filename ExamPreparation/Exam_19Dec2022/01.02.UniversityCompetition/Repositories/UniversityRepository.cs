using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        private readonly List<IUniversity> data;
        public UniversityRepository()
        {
            data = new List<IUniversity>();
        }
        public IReadOnlyCollection<IUniversity> Models
        {
            get => data;
        }
        public void AddModel(IUniversity model)
        {
            data.Add(model);
        }

        public IUniversity FindById(int id)
        {
            return data.FirstOrDefault(u => u.Id == id);
        }

        public IUniversity FindByName(string name)
        {
            return data.FirstOrDefault(u => u.Name == name);
        }
    }
}
