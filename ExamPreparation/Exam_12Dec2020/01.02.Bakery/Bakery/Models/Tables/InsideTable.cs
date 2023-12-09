namespace Bakery.Models.Tables
{
    public class InsideTable : Table
    {
        private const decimal InitialInsideTablePrice = 2.5m;
        public InsideTable(int tableNumber, int capacity)
            : base(tableNumber, capacity, InitialInsideTablePrice)
        {
        }
    }
}
