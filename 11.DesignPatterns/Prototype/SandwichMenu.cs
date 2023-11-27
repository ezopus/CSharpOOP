namespace PrototypePattern
{
    public class SandwichMenu
    {
        private Dictionary<string, SandwichPrototype> sandwiches = new();

        public SandwichPrototype this[string name]
        {
            get => sandwiches[name];
            set => sandwiches.Add(name, value);
        }
    }
}
