using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;

        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != nameof(BranchBank) && bankTypeName != nameof(CentralBank))
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            if (bankTypeName == nameof(BranchBank))
            {
                banks.AddModel(new BranchBank(name));
            }
            else
            {
                banks.AddModel(new CentralBank(name));
            }

            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }

        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != nameof(StudentLoan) && loanTypeName != nameof(MortgageLoan))
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }

            if (loanTypeName == nameof(StudentLoan))
            {
                loans.AddModel(new StudentLoan());
            }
            else
            {
                loans.AddModel(new MortgageLoan());
            }

            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank currentBank = banks.FirstModel(bankName);
            ILoan currentLoan = loans.FirstModel(loanTypeName);

            if (currentLoan == null)
            {
                return string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName);
            }

            if (loanTypeName == nameof(StudentLoan))
            {
                currentLoan = new StudentLoan();
            }
            else if (loanTypeName == nameof(MortgageLoan))
            {
                currentLoan = new MortgageLoan();
            }
            else
            {
                return string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName);
            }

            currentBank.AddLoan(currentLoan);
            loans.RemoveModel(currentLoan);
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, currentBank.Name);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IClient currentClient;

            if (clientTypeName != nameof(Student) && clientTypeName != nameof(Adult))
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IBank currentBank = banks.FirstModel(bankName);

            if (clientTypeName == nameof(Adult) && currentBank.GetType().Name == nameof(CentralBank))
            {
                currentClient = new Adult(clientName, id, income);
            }
            else if (clientTypeName == nameof(Student) && currentBank.GetType().Name == nameof(BranchBank))
            {
                currentClient = new Student(clientName, id, income);
            }
            else
            {
                return string.Format(OutputMessages.UnsuitableBank);
            }

            currentBank.AddClient(currentClient);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);

        }

        public string FinalCalculation(string bankName)
        {
            double sum = banks.Models.Where(b => b.Name == bankName).Sum(b => b.Clients.Sum(c => c.Income)) +
                         banks.Models.Where(b => b.Name == bankName).Sum(b => b.Loans.Sum(l => l.Amount));

            return string.Format(OutputMessages.BankFundsCalculated, bankName, $"{sum:f2}");
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
