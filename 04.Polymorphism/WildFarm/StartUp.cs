using WildFarm.Core;
using WildFarm.Core.Interfaces;
using WildFarm.Factories;
using WildFarm.Factories.Interfaces;
using WildFarm.IO;
using WildFarm.IO.Interfaces;

IReader reader = new Reader();
IWriter writer = new Writer();
IAnimalFactory animalFactory = new AnimalFactory();
IFoodFactory foodFactory = new FoodFactory();

IEngine engine = new Engine(reader, writer, animalFactory, foodFactory);

engine.Run();