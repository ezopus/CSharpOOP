using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private readonly List<IBakedFood> foods = new List<IBakedFood>();
        private readonly List<IDrink> drinks = new List<IDrink>();
        private int capacity;
        private int numberOfPeople;

        protected Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            TableNumber = tableNumber;
            Capacity = capacity;
            PricePerPerson = pricePerPerson;
        }

        public int TableNumber { get; private set; }
        public int Capacity
        {
            get => capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                capacity = value;
            }
        }
        public int NumberOfPeople
        {
            get => numberOfPeople;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                numberOfPeople = value;
            }
        }
        public decimal PricePerPerson { get; private set; }
        public bool IsReserved { get; private set; }
        public decimal Price => NumberOfPeople * PricePerPerson;
        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            this.numberOfPeople = numberOfPeople;
        }

        public void OrderFood(IBakedFood food)
        {
            foods.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            drinks.Add(drink);
        }

        public decimal GetBill()
        {
            decimal bill = foods.Sum(f => f.Price) + drinks.Sum(f => f.Price) + Price;
            return bill;
        }

        public void Clear()
        {
            foods.Clear();
            drinks.Clear();
            IsReserved = false;
            numberOfPeople = 0;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Price per Person: {PricePerPerson:f2}");

            return sb.ToString().Trim();
        }
    }
}
