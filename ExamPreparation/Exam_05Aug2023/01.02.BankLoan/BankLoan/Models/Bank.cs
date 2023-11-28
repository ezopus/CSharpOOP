using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private readonly List<ILoan> loans;
        private readonly List<IClient> clients;

        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }
        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }
        public IReadOnlyCollection<ILoan> Loans => loans;
        public IReadOnlyCollection<IClient> Clients => clients;
        public double SumRates()
        {
            return loans.Sum(l => l.InterestRate);
        }

        public void AddClient(IClient Client)
        {
            if (clients.Count == Capacity)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }

            clients.Add(Client);

        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}, Type: {this.GetType().Name}");
            if (clients.Any())
            {
                sb.AppendLine($"Clients: {string.Join(", ", clients.Select(cl => cl.Name))}");
            }
            else
            {
                sb.AppendLine("Clients: none");
            }
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {this.SumRates()}");

            return sb.ToString().TrimEnd();
        }
    }
}
