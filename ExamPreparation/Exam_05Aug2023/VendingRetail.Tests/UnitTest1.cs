using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {

        [SetUp]

        [Test]
        public void Constructor_InitializesCorrectly()
        {
            int expectedWaterCapacity = 0;
            int expectedButtonsCount = 10;

            CoffeeMat coffeeMat = new CoffeeMat(0, 10);

            Assert.IsNotNull(coffeeMat);
            Assert.AreEqual(0, coffeeMat.WaterCapacity);
            Assert.AreEqual(10, coffeeMat.ButtonsCount);
        }

        [Test]
        public void Income_GetsValueCorrectly()
        {
            CoffeeMat coffeMat = new CoffeeMat(100, 1);
            coffeMat.FillWaterTank();
            coffeMat.AddDrink("Coffee", 1.50);

            coffeMat.BuyDrink("Coffee");
            Assert.AreEqual(1.50, coffeMat.Income);
        }

        [Test]
        public void FillWaterTank_ReturnsCorrectMessage_WhenRefillingHappens()
        {
            int expectedWaterCapacity = 500;

            CoffeeMat coffeMat = new CoffeeMat(500, 10);

            string expectedResult = coffeMat.FillWaterTank();
            Assert.AreEqual($"Water tank is filled with 500ml", expectedResult);

        }
        [Test]
        public void FillWaterTank_ReturnsCorrectMessage_WhenAlreadyFull()
        {
            int expectedWaterCapacity = 500;

            CoffeeMat coffeMat = new CoffeeMat(500, 10);
            coffeMat.FillWaterTank();

            Assert.AreEqual($"Water tank is already full!", coffeMat.FillWaterTank());
        }

        [Test]
        public void AddDrink_SuccessfullyAddDrinks_WhenThereAreButtonsAvailable()
        {
            CoffeeMat coffeeMat = new CoffeeMat(500, 10);

            bool drinkAdded = coffeeMat.AddDrink("Coffee", 1.50);
            drinkAdded = coffeeMat.AddDrink("Tea", 1.20);
            Assert.IsTrue(drinkAdded);

            drinkAdded = coffeeMat.AddDrink("Latte", 1.70);
            Assert.IsTrue(drinkAdded);

        }
        [Test]
        public void AddDrink_FailsToAddDrink_WhenDrinkExists()
        {
            string drink = "Coffee";
            double price = 1.50;
            CoffeeMat coffeeMat = new CoffeeMat(500, 10);

            bool drinkAdded = coffeeMat.AddDrink(drink, price);

            Assert.IsTrue(drinkAdded);

            drinkAdded = coffeeMat.AddDrink(drink, price);

            Assert.IsFalse(drinkAdded);
        }

        [Test]
        public void AddDrink_FailsToAddDrink_WhenThereAreNoAvailableButtons()
        {
            string drink = "Coffee";
            double price = 1.50;
            CoffeeMat coffeeMat = new CoffeeMat(500, 1);
            coffeeMat.AddDrink(drink, price);

            bool drinkAdded = coffeeMat.AddDrink("Tea", 1.20);

            Assert.IsFalse(drinkAdded);
        }

        [Test]
        public void AddDrink_AddsCorrectDrinkAndPrice()
        {
            string drink = "Coffee";
            double price = 1.50;
            CoffeeMat coffeeMat = new CoffeeMat(500, 1);
            coffeeMat.AddDrink(drink, price);
            coffeeMat.FillWaterTank();
            double expectedIncome = 1.50;

            coffeeMat.BuyDrink("Coffee");

            Assert.AreEqual(expectedIncome, coffeeMat.Income);
        }

        [TestCase(1)]
        [TestCase(80)]
        public void BuyDrink_Fails_WhenThereIsNotEnoughWater(int water)
        {
            CoffeeMat coffeeMat = new CoffeeMat(water, 1);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Coffee", 1.50);
            coffeeMat.BuyDrink("Coffee");

            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("Coffee"));
        }

        [Test]
        public void BuyDrink_Fails_WhenThereIsNoSuchDrink()
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, 1);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Tea", 1.50);

            Assert.AreEqual($"Coffee is not available!", coffeeMat.BuyDrink("Coffee"));
        }

        [Test]
        public void BuyDrink_Successful_AndReturnsCorrectMessage()
        {
            string name = "Coffee";
            double expectedPrice = 1.50;
            CoffeeMat coffeeMat = new CoffeeMat(200, 1);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink(name, expectedPrice);

            string result = coffeeMat.BuyDrink("Coffee");

            Assert.AreEqual(1.50, coffeeMat.Income);
            Assert.AreEqual($"Your bill is 1.50$", result);
        }

        [Test]
        public void CollectIncome_WorksCorrectly()
        {
            double expectedResult = 4.5;
            CoffeeMat coffeeMat = new(500, 3);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Coffee", 1.50);
            coffeeMat.AddDrink("Tea", 1.50);
            coffeeMat.AddDrink("Latte", 1.50);

            coffeeMat.BuyDrink("Coffee");
            coffeeMat.BuyDrink("Tea");
            coffeeMat.BuyDrink("Latte");

            Assert.AreEqual(expectedResult, coffeeMat.CollectIncome());
            Assert.AreEqual(0, coffeeMat.Income);
        }

    }
}