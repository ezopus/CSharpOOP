using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Team_ConstructorWorksCorrectly()
        {
            string expectedName = "ManUtd";
            int capacity = 16;
            FootballTeam team = new FootballTeam("ManUtd", 16);

            Assert.AreEqual(expectedName, team.Name);
            Assert.AreEqual(capacity, team.Capacity);
            Assert.AreEqual(0, team.Players.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Team_Name_ThrowsException_WhenNullOrEmpty(string name)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam(name, 16));
            Assert.AreEqual("Name cannot be null or empty!", exception.Message);

        }
        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(14)]
        public void Team_Capacity_ThrowsException_WhenUnder15(int capacity)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new FootballTeam("ManUtd", capacity));
            Assert.AreEqual("Capacity min value = 15", exception.Message);
        }

        [Test]
        public void Team_AddPlayer_WorksCorrectly()
        {
            string playerName = "Gosho";
            int playerNumber = 7;
            string position = "Forward";
            FootballPlayer player = new FootballPlayer(playerName, playerNumber, position);
            FootballTeam team = new FootballTeam("ManUtd", 22);

            Assert.AreEqual($"Added player {playerName} in position {position} with number {playerNumber}", team.AddNewPlayer(player));
            Assert.AreEqual(1, team.Players.Count);
        }

        [Test]
        public void Team_AddPlayer_ReturnsCorrectMessage_WhenNotEnoughCapacity()
        {
            FootballPlayer player = new FootballPlayer("Gosho", 7, "Forward");
            FootballTeam team = new FootballTeam("ManUtd", 16);
            for (int i = 0; i < 16; i++)
            {
                team.AddNewPlayer(player);
            }

            Assert.AreEqual("No more positions available!", team.AddNewPlayer(player));
            Assert.AreEqual(16, team.Players.Count);
        }

        [Test]
        public void Team_PickPlayer_ReturnsCorrectPlayer()
        {
            string expectedPlayerName = "Gosho";
            FootballPlayer expectedPlayer = new FootballPlayer(expectedPlayerName, 7, "Forward");
            FootballPlayer player = new FootballPlayer("Ivan", 9, "Forward");
            FootballTeam team = new FootballTeam("ManUtd", 16);
            team.AddNewPlayer(expectedPlayer);
            team.AddNewPlayer(player);

            Assert.AreEqual(expectedPlayer, team.PickPlayer(expectedPlayerName));
        }

        [Test]
        public void Team_PickPlayer_ReturnsNullWhenPlayerNotFound()
        {
            FootballPlayer player = new FootballPlayer("Ivan", 9, "Forward");
            FootballTeam team = new FootballTeam("ManUtd", 16);
            team.AddNewPlayer(player);

            Assert.AreEqual(null, team.PickPlayer("Gosho"));
        }

        [Test]
        public void Team_PlayerScore_ReturnsCorrectPlayerNameWhenFound()
        {
            string playerName = "Gosho";
            int playerNumber = 7;
            string position = "Forward";
            FootballPlayer expectedPlayer = new FootballPlayer(playerName, playerNumber, position);
            FootballTeam team = new FootballTeam("ManUtd", 22);
            team.AddNewPlayer(expectedPlayer);

            Assert.AreEqual($"{playerName} scored and now has 1 for this season!", team.PlayerScore(playerNumber));
        }



    }
}