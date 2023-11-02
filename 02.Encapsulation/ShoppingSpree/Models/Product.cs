namespace ShoppingSpree.Models
{
    public class Product
    {
        private string name;

        public Product(string name, decimal cost)
        {
            this.name = name;
            Cost = cost;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }

        public decimal Cost { get; private set; }

        public override string ToString() => $"{Name}";
    }
}
