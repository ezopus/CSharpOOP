namespace TemplatePattern
{
    public class WholeWheat : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine("Gathering the ingredients for whole grain bread!");
        }

        public override void Bake()
        {
            Console.WriteLine($"Baking the whole grain bread. (15 minutes)");
        }
    }
}
