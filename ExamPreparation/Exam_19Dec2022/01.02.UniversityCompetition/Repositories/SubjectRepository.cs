using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private readonly List<ISubject> data;
        public SubjectRepository()
        {
            data = new List<ISubject>();
        }
        public IReadOnlyCollection<ISubject> Models
        {
            get => data;
        }
        public void AddModel(ISubject model)
        {
            data.Add(model);
        }

        public ISubject FindById(int id)
        {
            return data.FirstOrDefault(s => s.Id == id);
        }

        public ISubject FindByName(string name)
        {
            return data.FirstOrDefault(d => d.Name == name);
        }
    }
}
