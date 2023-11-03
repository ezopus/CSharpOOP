using CollectionHierarchy.Models;

namespace CollectionHierarchy
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            AddCollection addCollection = new();
            AddRemoveCollection addRemoveCollection = new();
            MyList myList = new();

            string[] input = Console.ReadLine().Split(" ");
            int removeOperations = int.Parse(Console.ReadLine());

            foreach (string inputItem in input)
            {
                Console.Write(addCollection.Add(inputItem) + " ");
            }

            Console.WriteLine();

            foreach (string inputItem in input)
            {
                Console.Write(addRemoveCollection.Add(inputItem) + " ");
            }

            Console.WriteLine();

            foreach (string inputItem in input)
            {
                Console.Write(myList.Add(inputItem) + " ");
            }
            
            Console.WriteLine();

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write(addRemoveCollection.Remove() + " ");
            }

            Console.WriteLine();

            for (int i = 0; i < removeOperations; i++)
            {
                Console.Write(myList.Remove() + " ");
            }
        }
    }
}