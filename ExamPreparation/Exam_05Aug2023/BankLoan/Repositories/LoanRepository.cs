using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private readonly List<ILoan> loanRepository;

        public LoanRepository()
        {
            loanRepository = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => loanRepository;
        public void AddModel(ILoan model)
        {
            loanRepository.Add(model);
        }
        public bool RemoveModel(ILoan model)
        {
            ILoan removed = loanRepository.FirstOrDefault(model);
            return loanRepository.Remove(removed);
        }
        public ILoan FirstModel(string name)
        {
            return loanRepository.FirstOrDefault(lm => lm.GetType().Name == name);
        }
    }
}
