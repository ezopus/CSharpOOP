namespace SimpleSnake.GameObjects.Foods
{
    public class DollarFood : Food
    {
        private const char DollarFoodSymbol = '$';
        private const int DollarFoodPoints = 2;

        public DollarFood(Wall wall)
            : base(wall, DollarFoodSymbol, DollarFoodPoints)
        {
        }
    }
}
