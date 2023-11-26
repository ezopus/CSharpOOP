using System;
using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private readonly List<IStudent> data;
        public StudentRepository()
        {
            data = new List<IStudent>();
        }
        public IReadOnlyCollection<IStudent> Models
        {
            get => data;
        }
        public void AddModel(IStudent model)
        {
            data.Add(model);
        }

        public IStudent FindById(int id)
        {
            return data.FirstOrDefault(s => s.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] names = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string firstName = names[0];
            string lastName = names[1];
            return data.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
