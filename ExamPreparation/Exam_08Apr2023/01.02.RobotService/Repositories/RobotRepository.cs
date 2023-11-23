using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> data;
        public RobotRepository()
        {
            data = new List<IRobot>();
        }
        public IReadOnlyCollection<IRobot> Models()
        {
            return data.AsReadOnly();
        }

        public void AddNew(IRobot model)
        {
            data.Add(model);
        }

        public bool RemoveByName(string typeName)
        {
            return data.Remove(data.FirstOrDefault(d => d.GetType().Name == typeName));
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return data.FirstOrDefault(d => d.InterfaceStandards.Contains(interfaceStandard));
        }
    }
}
