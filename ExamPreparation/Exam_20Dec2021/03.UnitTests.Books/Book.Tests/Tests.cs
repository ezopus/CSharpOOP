using System;

namespace Book.Tests
{
    using NUnit.Framework;

    public class Tests
    {
        [Test]
        public void Constructor_WorksPropertly()
        {
            Book book = new Book("Ring", "King");
            Assert.IsNotNull(book);
            Assert.AreEqual("Ring", book.BookName);
            Assert.AreEqual("King", book.Author);
        }
        [Test]
        public void Constructor_FootnoteListInitializesCorrectly()
        {
            Book book = new Book("Ring", "King");
            Assert.IsNotNull(book.FootnoteCount);
            Assert.AreEqual(0, book.FootnoteCount);
        }
        [TestCase(null)]
        [TestCase((""))]
        public void BookName_ThrowsException_WhenNullOrEmpty(string author)
        {
            string expectedResult = "Invalid Author!";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Book("Ring", author));

            Assert.AreEqual(expectedResult, ex.Message);
        }
        [TestCase(null)]
        [TestCase((""))]
        public void BookAuthor_ThrowsException_WhenNullOrEmpty(string name)
        {
            string expectedResult = "Invalid BookName!";

            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
                new Book(name, "King"));

            Assert.AreEqual(expectedResult, ex.Message);
        }

        [Test]
        public void AddFootnote_WorksProperly()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            Assert.AreEqual(1, book.FootnoteCount);
        }

        [Test]
        public void AddFootnote_ThrowsException_WhenFootnoteNumberAlreadyPresent()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            string expectedMessage = "Footnote already exists!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => book.AddFootnote(10, "Text"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void FindFootnote_WorksCorrectly()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            string expectedMessage = "Footnote #10: My footnote";

            Assert.AreEqual(expectedMessage, book.FindFootnote(10));
        }

        [Test]
        public void FindFootnote_ThrowsException_WhenFootnoteNumberDoesntExist()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            string expectedMessage = "Footnote doesn't exists!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => book.FindFootnote(5));
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [Test]
        public void AlterFootnote_WorksCorrectly()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            book.AlterFootnote(10, "Text");
            string expectedMessage = "Footnote #10: Text";

            Assert.AreEqual(expectedMessage, book.FindFootnote(10));
        }

        [Test]
        public void AlterFootnote_ThrowsException_WhenFootnoteNumberDoesntExist()
        {
            Book book = new Book("Ring", "King");
            book.AddFootnote(10, "My footnote");

            string expectedMessage = "Footnote does not exists!";

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(5, "Text"));
            Assert.AreEqual(expectedMessage, ex.Message);
        }
    }
}