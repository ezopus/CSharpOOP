namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int studentRate = 1;
        private const double studentAmount = 10000d;
        public StudentLoan()
            : base(studentRate, studentAmount)
        {
        }
    }
}
