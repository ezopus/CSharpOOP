namespace BankLoan.Models
{
    public class Adult : Client
    {
        private const int DefaultInterest = 4;
        public Adult(string name, string id, double income)
            : base(name, id, DefaultInterest, income)
        {
        }

        public override void IncreaseInterest()
        {
            this.Interest += 2;
        }
    }
}
