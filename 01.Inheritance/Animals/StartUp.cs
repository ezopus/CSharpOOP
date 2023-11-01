using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new();
            string input;

            while ((input = Console.ReadLine()) != "Beast!")
            {
                string animalType = input;
                string[] animalTokens = Console
                    .ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                switch (animalType)
                {
                    case "Dog":
                        Dog dog = new Dog(animalTokens[0], int.Parse(animalTokens[1]), animalTokens[2]);
                        animals.Add(dog);
                        break;
                    case "Cat":
                        Cat cat = new Cat(animalTokens[0], int.Parse(animalTokens[1]), animalTokens[2]);
                        animals.Add(cat);
                        break;
                    case "Kitten":
                        Kitten kitten = new(animalTokens[0], int.Parse(animalTokens[1]));
                        animals.Add(kitten);
                        break;
                    case "Tomcat":
                        Tomcat tomcat = new(animalTokens[0], int.Parse(animalTokens[1]));
                        animals.Add(tomcat);
                        break;
                    case "Frog":
                        Frog frog = new Frog(animalTokens[0], int.Parse(animalTokens[1]), animalTokens[2]);
                        animals.Add(frog);
                        break;
                }

            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.GetType().Name);
                Console.WriteLine($"{animal.Name} {animal.Age} {animal.Gender}");
                Console.WriteLine(animal.ProduceSound());
            }
        }
    }
}
