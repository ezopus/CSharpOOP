namespace FacadePattern
{
    public class Car
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public int Doors { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public override string ToString()
        {
            return $"Car: {Type}, Color: {Color}, Doors: {Doors}, Manufactured in {City}, at address {Address}";
        }
    }
}
