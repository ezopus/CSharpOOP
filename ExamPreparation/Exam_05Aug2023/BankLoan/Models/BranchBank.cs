namespace BankLoan.Models
{
    public class BranchBank : Bank
    {
        private const int BankCapacity = 25;
        public BranchBank(string name)
            : base(name, BankCapacity)
        {

        }
    }
}
