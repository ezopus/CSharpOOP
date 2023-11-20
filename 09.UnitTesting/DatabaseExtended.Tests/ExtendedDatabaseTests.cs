using ExtendedDatabase;
using System;

namespace DatabaseExtended.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        [Test]
        public void Database_ConstructorInitializesCorrectly()
        {
            Person person = new(123456, "Gosho");
            int expectedCount = 1;

            database = new Database(person);

            Assert.IsNotNull(database);
            Assert.AreEqual(expectedCount, database.Count);
        }

        [Test]
        public void Database_AddRange_WorksCorrectly()
        {
            Person[] persons = new Person[] {
                new Person( 123456, "Gosho" ),
                new Person(123457, "Pesho"),
                new Person(123, "Ivan")
            };
            database = new Database(persons);

            Assert.AreEqual(persons.Length, database.Count);
            //InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
            //    () => database = new Database(data));

            //Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void Database_AddRange_ThrowsExceptionWhenRangeOver16()
        {
            Person[] persons = new Person[20];
            for (int i = 0; i < 17; i++)
            {
                Person newPerson = new Person(i, $"Person{i}");
                persons[i] = newPerson;
            }

            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => database = new Database(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }

        [Test]
        public void Database_Add_ThrowsExceptionWhenRangeOver16()
        {
            database = new Database();
            for (int i = 0; i < 16; i++)
            {
                Person newPerson = new Person(i, $"Person{i}");
                database.Add(newPerson);
            }

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.Add(new Person(1, "user")));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [TestCase("Ivan")]
        public void Database_Add_ThrowsExceptionWhen_PersonWithSameNameAlreadyExists(string name)
        {
            Person person = new Person(1, name);
            Person person2 = new Person(1, name);
            database = new Database();

            database.Add(person);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.Add(person2));

            Assert.AreEqual("There is already user with this username!", ex.Message);
        }

        [TestCase(123123123123123123)]
        public void Database_Add_ThrowsExceptionWhen_PersonWithSameIDAlreadyExists(long id)
        {
            Person person = new Person(id, "Gosho");
            Person person2 = new Person(id, "Pesho");
            database = new Database();

            database.Add(person);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.Add(person2));

            Assert.AreEqual("There is already user with this Id!", ex.Message);
        }

        [Test]
        public void Database_RemoveMethod_WorksCorrectly()
        {
            Person person = new Person(123, "Gosho");
            Person person2 = new Person(345, "Pesho");

            database = new Database();
            database.Add(person);
            database.Add(person2);

            int expectedCount = 1;

            database.Remove();

            Assert.AreEqual(expectedCount, database.Count);
        }
        [Test]
        public void Database_RemoveMethod_ThrowsException()
        {
            Person person = new Person(123, "Gosho");

            database = new Database();
            database.Add(person);

            database.Remove();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [TestCase("Gosho")]
        public void Database_FindByUsernameWorksCorrectly(string username)
        {
            Person person = new Person(123, "Gosho");
            database = new Database();
            database.Add(person);

            Assert.AreEqual(person, database.FindByUsername(username));
        }

        [TestCase("Gosho")]
        public void Database_FindByUsernameThrowsExceptionWhenUserNotFound(string username)
        {
            Person person = new Person(123, "Pesho");
            database = new Database();
            database.Add(person);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.FindByUsername(username));
            Assert.AreEqual("No user is present by this username!", ex.Message);
        }

        [Test]
        public void Database_FindByUsernameThrowsExceptionWhenUsernameNull()
        {
            string username = String.Empty;
            database = new Database();

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(username));
            Assert.AreEqual("Username parameter is null!", ex.ParamName);
        }

        [TestCase(123456)]
        public void Database_FindByIDWorksCorrectly(long id)
        {
            Person person = new Person(id, "Gosho");
            database = new Database();
            database.Add(person);

            Assert.AreEqual(person, database.FindById(id));
        }

        [TestCase(123456)]
        public void Database_FindByIDThrowsExceptionWhenIDNotFound(long id)
        {
            Person person = new Person(123, "Pesho");
            database = new Database();
            database.Add(person);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.FindById(id));
            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }

        [TestCase(-1)]
        [TestCase(-1111111111111111111)]
        public void Database_FindByIDThrowsExceptionWhenIdIsNegative(long id)
        {
            database = new Database();

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(id));
            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }


    }
}