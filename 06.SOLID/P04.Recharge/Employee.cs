namespace P04.Recharge
{
    public class Employee : Worker, ISleeper
    {
        public Employee(string id)
            : base(id)
        {
        }

        public virtual void Sleep()
        {
            // sleep...
        }

        //public override void Recharge()
        //{
        //    throw new InvalidOperationException("Employees cannot recharge");
        //}

    }
}
