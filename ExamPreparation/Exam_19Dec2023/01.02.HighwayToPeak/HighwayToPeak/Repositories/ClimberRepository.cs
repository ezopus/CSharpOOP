using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class ClimberRepository : IRepository<IClimber>
    {
        private readonly List<IClimber> all;

        public ClimberRepository()
        {
            all = new List<IClimber>();
        }

        public IReadOnlyCollection<IClimber> All => all;
        public void Add(IClimber model) => all.Add(model);

        public IClimber Get(string name) => all.FirstOrDefault(x => x.Name == name);
    }
}
