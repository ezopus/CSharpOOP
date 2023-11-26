using System;
using System.Linq;

namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_InitializesProperly()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.IsNotNull(library);
        }

        [Test]
        public void Field_Catalogue_IsNotNullWhenInitialzied()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.IsNotNull(library.Catalogue);
            Assert.AreEqual(0, library.Catalogue.Count);
        }

        [Test]
        public void AddTextBookToLibrary_AddsCorrectly()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");

            library.AddTextBookToLibrary(book);

            Assert.AreEqual(1, library.Catalogue.Count);
        }

        [Test]
        public void AddedTextBookInventoryNumber_IsCorrect()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");

            library.AddTextBookToLibrary(book);

            Assert.AreEqual(book, library.Catalogue.Find(b => b.InventoryNumber == 1));
        }

        [Test]
        public void AddTextBookToLibrary_ReturnsCorrectString()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");

            string result = library.AddTextBookToLibrary(book);
            string expectedResult = book.ToString();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void LoanTextBook_ReturnsCorrectMessage_IfTextBookExists()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");
            string studentName = "Gosho";

            book.Holder = "Ivan";
            library.AddTextBookToLibrary(book);

            string expectedResult = $"{book.Title} loaned to {studentName}.";
            string actualResult = library.LoanTextBook(1, studentName);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void LoanTextBook_ReturnsCorrectMessage_IfTextBookHasNotBeenReturned()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");
            string studentName = "Gosho";

            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, studentName);

            string expectedResult = $"Gosho still hasn't returned BookTitle!";
            string actualResult = library.LoanTextBook(1, studentName);

            Assert.IsTrue(library.Catalogue.Contains(book));
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual("Gosho", book.Holder);
        }

        [Test]
        public void LoanTextBook_ThrowsException_WhenBookIsNotExisting()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.IsNull(library.Catalogue.FirstOrDefault(b => b.InventoryNumber == 1));
            Assert.Throws<NullReferenceException>(() => library.LoanTextBook(1, "Gosho"));
        }

        [Test]
        public void ReturnTextBook_ReturnsCorrectString_WhenBookIsReturned()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("BookTitle", "BookAuthor", "Fantasy");
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Ivan");

            string expectedResult = $"BookTitle is returned to the library.";
            string actualResult = library.ReturnTextBook(1);

            Assert.IsTrue(library.Catalogue.Contains(book));
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(string.Empty, book.Holder);
        }

        [Test]
        public void ReturnTextBook_ThrowsException_WhenBookIsNotExisting()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.IsNull(library.Catalogue.FirstOrDefault(b => b.InventoryNumber == 1));
            Assert.Throws<NullReferenceException>(() => library.ReturnTextBook(1));
        }
    }
}