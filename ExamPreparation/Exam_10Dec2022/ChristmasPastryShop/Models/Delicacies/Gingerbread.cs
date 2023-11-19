namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        private const double GingerbreadPrice = 4.0;
        public Gingerbread(string name)
            : base(name, GingerbreadPrice)
        {

        }

    }
}
