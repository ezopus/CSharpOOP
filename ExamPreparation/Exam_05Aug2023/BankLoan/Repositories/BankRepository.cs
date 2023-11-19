using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private readonly List<IBank> bankRepository;

        public BankRepository()
        {
            bankRepository = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => bankRepository;
        public void AddModel(IBank model)
        {
            bankRepository.Add(model);
        }
        public bool RemoveModel(IBank model)
        {
            return bankRepository.Remove(model);
        }
        public IBank FirstModel(string name)
        {
            return bankRepository.FirstOrDefault(b => b.Name == name);
        }
    }
}
