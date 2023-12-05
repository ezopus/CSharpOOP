namespace SimpleSnake.GameObjects
{
    public class Wall : Point
    {
        private const char WallSymbol = '\u25A0';
        public Wall(int leftX, int topY)
            : base(leftX, topY)
        {
            InitializeBorders();
        }

        public bool IsPointOfWall(Point snakeHead)
            => snakeHead.TopY == 0
               || snakeHead.TopY == TopY
               || snakeHead.LeftX == 0
               || snakeHead.LeftX == LeftX - 1;
        public void InitializeBorders()
        {
            DrawHorizontalLine(0);
            DrawHorizontalLine(this.TopY);

            DrawVerticalLine(0);
            DrawVerticalLine(this.LeftX - 1);
        }
        public void DrawHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX < LeftX; leftX++)
            {
                Draw(leftX, topY, WallSymbol);
            }
        }

        private void DrawVerticalLine(int leftX)
        {
            for (int topY = 0; topY < this.TopY; topY++)
            {
                Draw(leftX, topY, WallSymbol);
            }
        }
    }
}
