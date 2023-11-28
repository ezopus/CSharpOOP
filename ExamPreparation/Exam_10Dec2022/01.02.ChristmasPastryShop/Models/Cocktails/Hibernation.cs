namespace ChristmasPastryShop.Models.Cocktails
{
    public class Hibernation : Cocktail
    {
        private const double LargeHibernationPrice = 10.5;
        public Hibernation(string name, string size)
            : base(name, size, LargeHibernationPrice)
        {

        }
    }
}
