namespace SimpleSnake.GameObjects.Foods
{
    public class HashFood : Food
    {
        private const char HasFoodSymbol = '#';
        private const int HashFoodPoints = 3;

        public HashFood(Wall wall)
            : base(wall, HasFoodSymbol, HashFoodPoints)
        {
        }
    }
}
