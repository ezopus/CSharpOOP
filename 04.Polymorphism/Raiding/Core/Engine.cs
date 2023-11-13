using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICollection<IBaseHero> heroes;

        private readonly IHeroFactory heroFactory;
        public Engine(IReader reader, IWriter writer, IHeroFactory heroFactory)
        {
            this.writer = writer;
            this.reader = reader;
            this.heroFactory = heroFactory;
            heroes = new List<IBaseHero>();
        }
        public void Run()
        {
            int numberOfHeroes = int.Parse(reader.ReadLine());

            while (numberOfHeroes > 0)
            {
                try
                {
                    IBaseHero currentHero = CreateHero();
                    heroes.Add(currentHero);
                    numberOfHeroes--;
                }
                catch (ArgumentException e)
                {
                    writer.WriteLine(e.Message);
                }
            }

            int bossPower = int.Parse(reader.ReadLine());
            int totalPower = 0;

            foreach (var hero in heroes)
            {
                writer.WriteLine(hero.CastAbility());
                totalPower += hero.Power;
            }

            if (totalPower >= bossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }
        }

        private IBaseHero CreateHero()
        {
            string heroName = reader.ReadLine();
            string heroType = reader.ReadLine();

            return heroFactory.CreateHero(heroName, heroType);

        }

    }
}



