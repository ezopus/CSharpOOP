namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        private const decimal InitialOutsideTablePrice = 3.5m;
        public OutsideTable(int tableNumber, int capacity)
            : base(tableNumber, capacity, InitialOutsideTablePrice)
        {
        }
    }
}
