namespace SquareRoot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            
            try
            {
                Console.WriteLine(Root(number));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }

        static public double Root(double n)
        {
            if (n >= 0)
            {
                return Math.Sqrt(n);
            }

            throw new ArgumentException("Invalid number.");
        }
    }
}