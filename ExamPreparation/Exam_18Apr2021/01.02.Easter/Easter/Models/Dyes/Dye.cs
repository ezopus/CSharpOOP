using Easter.Models.Dyes.Contracts;

namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        private int power;
        public Dye(int power)
        {
            Power = power;
        }

        public int Power
        {
            get => power;
            private set
            {
                if (power < 0)
                {
                    power = 0;
                }
                else
                {
                    power = value;
                }
            }
        }
        public void Use()
        {
            Power -= 10;
        }

        public bool IsFinished()
        {
            return Power == 0;
        }
    }
}
