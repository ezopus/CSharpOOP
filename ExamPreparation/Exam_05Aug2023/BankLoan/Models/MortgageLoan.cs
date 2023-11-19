namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int mortgageRate = 3;
        private const double mortgageAmount = 50000d;
        public MortgageLoan()
            : base(mortgageRate, mortgageAmount)
        {
        }
    }
}
