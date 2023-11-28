namespace BankLoan.Models
{
    public class CentralBank : Bank
    {
        private const int BankCapacity = 50;
        public CentralBank(string name)
            : base(name, BankCapacity)
        {

        }
    }
}
