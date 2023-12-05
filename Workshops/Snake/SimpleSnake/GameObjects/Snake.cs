using SimpleSnake.GameObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSnake.GameObjects
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';
        private const char EmptySymbol = ' ';

        private readonly Wall wall;
        private readonly Queue<Point> snakeElements;
        private readonly List<Food> foods;

        private int nextLeftX;
        private int nextTopY;
        private int foodIndex;

        public Snake(Wall wall)
        {
            this.wall = wall;
            snakeElements = new Queue<Point>();
            foods = new List<Food>();
            foodIndex = RandomFoodNumber;

            GetFoods();
            CreateFood();
            CreateSnake();
        }

        public int FoodEaten { get; set; }
        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = snakeElements.Last();
            GetNextPoint(direction, currentSnakeHead);

            bool isPointOfSnake = snakeElements.Any(s => s.LeftX == nextLeftX && s.TopY == nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point newSnakeHead = new Point(nextLeftX, nextTopY);

            if (wall.IsPointOfWall(newSnakeHead))
            {
                return false;
            }

            snakeElements.Enqueue(newSnakeHead);
            newSnakeHead.Draw(snakeSymbol);

            if (foods[foodIndex].IsFoodPoint(newSnakeHead))
            {
                Eat(direction, currentSnakeHead);
            }

            Point snakeTail = snakeElements.Dequeue();
            snakeTail.Draw(EmptySymbol);

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = foods[foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                snakeElements.Enqueue(new Point(nextLeftX, nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            FoodEaten += length;

            CreateFood();
        }

        private void CreateFood()
        {
            foodIndex = RandomFoodNumber;
            foods[foodIndex].SetRandomPosition(snakeElements);
        }

        public int RandomFoodNumber => new Random().Next(0, 2);

        public void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                snakeElements.Enqueue(new Point(2, topY));
            }
        }

        private void GetFoods()
        {
            foods.Add(new AsteriskFood(wall));
            foods.Add(new DollarFood(wall));
            foods.Add(new HashFood(wall));
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            nextLeftX = snakeHead.LeftX + direction.LeftX;
            nextTopY = snakeHead.TopY + direction.TopY;
        }
    }
}
