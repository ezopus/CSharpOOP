using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects.Foods
{
    public abstract class Food : Point
    {
        private char foodSymbol;
        private Random random;
        private Wall wall;
        protected Food(Wall wall, char foodSymbol, int points)
            : base(wall.LeftX, wall.TopY)
        {
            random = new Random();
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            FoodPoints = points;
        }
        public int FoodPoints { get; protected set; }

        public void SetRandomPosition(Queue<Point> snakeElements)
        {
            do
            {
                LeftX = random.Next(2, wall.LeftX - 2);
                TopY = random.Next(2, wall.TopY - 2);
            } while (snakeElements.Any(s => s.LeftX == LeftX && s.TopY == TopY));

            Console.BackgroundColor = ConsoleColor.Red;
            Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snakeHead)
            => snakeHead.LeftX == LeftX && snakeHead.TopY == TopY;
    }
}
