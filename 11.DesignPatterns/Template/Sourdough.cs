namespace TemplatePattern
{
    public class Sourdough : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine("Gathering the ingredients for sourdough bread!");
        }

        public override void Bake()
        {
            Console.WriteLine($"Baking the sourdough bread. (20 minutes)");
        }
    }
}
