namespace P04.Recharge
{
    public class Robot : Worker, IRechargeable
    {
        private int capacity;
        private int currentPower;

        public Robot(string id, int capacity)
            : base(id)
        {
            this.capacity = capacity;
        }

        public int Capacity => capacity;

        public int CurrentPower
        {
            get => currentPower;
            set => currentPower = value;
        }

        public void Work(int hours)
        {
            if (hours > currentPower)
            {
                hours = currentPower;
            }

            base.Work(hours);
            currentPower -= hours;
        }

        public virtual void Recharge()
        {
            currentPower = capacity;
        }

        /*         
           public override void Sleep()
           {
           throw new InvalidOperationException("Robots cannot sleep");
           }
         */
    }
}