using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private readonly List<IBakedFood> foods = new List<IBakedFood>();
        private readonly List<IDrink> drinks = new List<IDrink>();
        private readonly List<ITable> tables = new List<ITable>();
        private decimal totalIncome;
        public Controller()
        {
        }
        public string AddFood(string type, string name, decimal price)
        {
            if (type == nameof(Bread) || type == nameof(Cake))
            {
                IBakedFood food;
                if (type == nameof(Bread))
                {
                    food = new Bread(name, price);
                }
                else
                {
                    food = new Cake(name, price);
                }

                foods.Add(food);
                return string.Format(OutputMessages.FoodAdded, name, type);
            }

            return null;
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            if (type == nameof(Tea) || type == nameof(Water))
            {
                IDrink drink;
                if (type == nameof(Water))
                {
                    drink = new Water(name, portion, brand);
                }
                else
                {
                    drink = new Tea(name, portion, brand);
                }

                drinks.Add(drink);
                return string.Format(OutputMessages.DrinkAdded, name, brand);
            }

            return null;
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null;
            if (type == nameof(InsideTable))
            {
                table = new InsideTable(tableNumber, capacity);
            }
            else if (type == nameof(OutsideTable))
            {
                table = new OutsideTable(tableNumber, capacity);
            }

            tables.Add(table);
            return string.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable currentTable = tables.FirstOrDefault(t => t.IsReserved == false && t.Capacity >= numberOfPeople);
            if (currentTable == null)
            {
                return string.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }
            else
            {
                currentTable.Reserve(numberOfPeople);
                return string.Format(OutputMessages.TableReserved, currentTable.TableNumber, numberOfPeople);
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable currentTable = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (currentTable == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }
            IBakedFood foodOrder = foods.FirstOrDefault(f => f.Name == foodName);

            if (foodOrder == null)
            {
                return string.Format(OutputMessages.NonExistentFood, foodName);
            }

            currentTable.OrderFood(foodOrder);

            return string.Format(OutputMessages.FoodOrderSuccessful, currentTable.TableNumber, foodName);
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable currentTable = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (currentTable == null)
            {
                return string.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IDrink drinkOrder = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand == drinkBrand);

            if (drinkOrder == null)
            {
                return string.Format(OutputMessages.NonExistentDrink, drinkName, drinkBrand);
            }

            currentTable.OrderDrink(drinkOrder);

            return string.Format(OutputMessages.DrinkOrderSuccessful, currentTable.TableNumber, drinkName, drinkBrand);
        }

        public string LeaveTable(int tableNumber)
        {
            ITable currentTable = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            decimal currentBill = currentTable.GetBill();
            totalIncome += currentBill;

            currentTable.Clear();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {currentTable.TableNumber}");
            sb.AppendLine($"Bill: {currentBill:f2}");

            return sb.ToString().Trim();
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var table in tables.Where(t => t.IsReserved == false))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }
            return sb.ToString().Trim();
        }

        public string GetTotalIncome()
        {
            return string.Format(OutputMessages.TotalIncome, $"{totalIncome:f2}");
        }
    }
}
