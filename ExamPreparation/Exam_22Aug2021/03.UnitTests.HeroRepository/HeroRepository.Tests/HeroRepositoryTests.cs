using NUnit.Framework;
using System;

public class HeroRepositoryTests
{
    [Test]
    public void Constructor_InitializesProperly()
    {
        HeroRepository repository = new HeroRepository();
        Assert.IsNotNull(repository);
    }

    [Test]
    public void Constructor_DataList_InititalizesProperly()
    {
        HeroRepository repository = new HeroRepository();
        Assert.IsNotNull(repository.Heroes);
        Assert.AreEqual(0, repository.Heroes.Count);
    }

    [Test]
    public void Create_WorksProperly()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);

        string expectedMessage = "Successfully added hero Flash with level 20";
        Assert.AreEqual(expectedMessage, repository.Create(hero));
        Assert.AreEqual(1, repository.Heroes.Count);
    }

    [Test]
    public void Create_ThrowsExceptionWhen_HeroIsNull()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = null;

        string expectedMessage = "Hero is null (Parameter 'hero')";

        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(()
            => repository.Create(hero));
        Assert.AreEqual(expectedMessage, ex.Message);
    }

    [Test]
    public void Create_ThrowsExceptionWhen_HeroAlreadyCreated()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);
        repository.Create(hero);

        string expectedMessage = "Hero with name Flash already exists";

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
            => repository.Create(hero));
        Assert.AreEqual(expectedMessage, ex.Message);
    }

    [Test]
    public void Remove_WorksProperly_ReturnsTrue()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);
        repository.Create(hero);

        Assert.IsTrue(repository.Remove("Flash"));
        Assert.AreEqual(0, repository.Heroes.Count);
    }
    [Test]
    public void Remove_WorksProperly_ReturnsFalse()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);
        repository.Create(hero);

        Assert.IsFalse(repository.Remove("Batman"));
        Assert.AreEqual(1, repository.Heroes.Count);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("     ")]
    [TestCase(null)]
    public void Remove_ThrowsExceptionWhen_HeroNameIsNull(string name)
    {
        HeroRepository repository = new HeroRepository();
        string expectedMessage = "Name cannot be null (Parameter 'name')";

        ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => repository.Remove(name));
        Assert.AreEqual(expectedMessage, ex.Message);
    }

    [Test]
    public void GetHeroWithHighestLevel_ReturnsCorrectHero()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);
        Hero hero2 = new Hero("Batman", 50);
        Hero hero3 = new Hero("Superman", 99);
        repository.Create(hero);
        repository.Create(hero2);
        repository.Create(hero3);

        Hero expectedHero = repository.GetHeroWithHighestLevel();
        Assert.AreEqual(hero3, expectedHero);
        Assert.AreEqual(99, expectedHero.Level);
    }
    [Test]
    public void GetHero_ReturnsCorrectHero()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = new Hero("Flash", 20);
        Hero hero2 = new Hero("Batman", 50);
        Hero hero3 = new Hero("Superman", 99);
        repository.Create(hero);
        repository.Create(hero2);
        repository.Create(hero3);

        Hero expectedHero = repository.GetHero("Batman");
        Assert.AreEqual(hero2, expectedHero);
        Assert.AreEqual(hero2.Name, expectedHero.Name);
        Assert.AreEqual(hero2.Level, expectedHero.Level);
    }

    [Test]
    public void GetHeroWithHighestLevel_ReturnsCorrectHeroWhenNull()
    {
        HeroRepository repository = new HeroRepository();
        Hero hero = null;

        Assert.Throws<IndexOutOfRangeException>(() => repository.GetHeroWithHighestLevel());
    }
}