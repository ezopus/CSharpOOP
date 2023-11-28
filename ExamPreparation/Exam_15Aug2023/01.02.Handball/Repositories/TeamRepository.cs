using Handball.Models.Contracts;
using Handball.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private List<ITeam> teams;

        public TeamRepository()
        {
            teams = new List<ITeam>();
        }
        public IReadOnlyCollection<ITeam> Models
        {
            get => teams.AsReadOnly();
        }

        public void AddModel(ITeam model)
        {
            teams.Add(model);
        }

        public bool RemoveModel(string name)
        {
            return teams.Remove(teams.FirstOrDefault(p => p.Name == name));
        }

        public bool ExistsModel(string name)
        {
            return teams.Any(p => p.Name == name);
        }

        public ITeam GetModel(string name)
        {
            return teams.FirstOrDefault(p => p.Name == name);
        }
    }
}
