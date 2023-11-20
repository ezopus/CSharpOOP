using System;

namespace Database.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [TestCase(new int[] { 1, 2 })]
        public void Database_ConstructorInitializesCorrectly(int[] data)
        {
            int expectedCount = data.Length;

            database = new Database(data);

            Assert.IsNotNull(database);
            Assert.AreEqual(expectedCount, database.Count);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        public void Database_InitializeDatabaseThrowsException_WhenMoreThan16(int[] data)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database = new Database(data));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);

        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        public void Database_AddMethodWorksCorrectly(int[] data)
        {
            database = new();
            foreach (var number in data)
            {
                database.Add(number);
            }
            Assert.AreEqual(data.Length, database.Count);
            Assert.AreEqual(data, database.Fetch());

        }

        [Test]
        public void Database_AddMethodThrowsException_WhenElementsOver16()
        {
            database = new Database();

            for (int i = 0; i < 16; i++)
            {
                database.Add(i);
            }

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.Add(1));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);

        }

        [TestCase(new int[] { 1, 2 })]
        public void Database_RemoveMethodWorksCorrectly(int[] data)
        {
            database = new Database(data);
            int expectedResult = 1;

            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);

        }

        [TestCase(new int[] { 1 })]
        public void Database_RemoveMethodThrowsException_WhenEmpty(int[] data)
        {
            database = new Database(data);
            int expectedResult = 1;

            database.Remove();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.Remove());

            Assert.AreEqual("The collection is empty!", ex.Message);
        }


    }
}
