namespace BankLoan.Models
{
    public class Student : Client
    {
        private const int DefaultInterest = 2;
        public Student(string name, string id, double income)
            : base(name, id, DefaultInterest, income)
        {
        }
        public override void IncreaseInterest()
        {
            this.Interest += 1;
        }
    }
}
