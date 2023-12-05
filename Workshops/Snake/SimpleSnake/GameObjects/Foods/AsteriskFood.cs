namespace SimpleSnake.GameObjects.Foods
{
    public class AsteriskFood : Food
    {
        private const char AsteriskFoodSymbol = '*';
        private const int AsteriskFoodPoints = 1;

        public AsteriskFood(Wall wall)
            : base(wall, AsteriskFoodSymbol, AsteriskFoodPoints)
        {
        }
    }
}
