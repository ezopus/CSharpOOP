using Raiding.Core;
using Raiding.Core.Interfaces;
using Raiding.Factories;
using Raiding.Factories.Interfaces;
using Raiding.IO;
using Raiding.IO.Interfaces;

IReader reader = new Reader();
IWriter writer = new Writer();
IHeroFactory heroFactory = new HeroFactory();
IEngine engine = new Engine(reader, writer, heroFactory);

engine.Run();

