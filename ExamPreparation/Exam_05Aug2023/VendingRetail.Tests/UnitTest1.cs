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

            Assert.AreEqual(expectedWaterCapacity, coffeeMat.WaterCapacity);
            Assert.AreEqual(expectedButtonsCount, coffeeMat.ButtonsCount);
            Assert.AreEqual(0, coffeeMat.Income);
            Assert.AreEqual(0, coffeeMat.CollectIncome());
            Assert.AreEqual("Water tank is already full!", coffeeMat.FillWaterTank());
        }

        [Test]
        public void Income_GetsValueCorrectly()
        {
            CoffeeMat coffeMat = new CoffeeMat(100, 1);
            coffeMat.AddDrink("Coffee", 1.50);

            Assert.AreEqual(0, coffeMat.Income);

            coffeMat.BuyDrink("Coffee");
            coffeMat.CollectIncome();

            Assert.AreEqual(0, coffeMat.Income);
        }

        [Test]
        public void FillWaterTank_ReturnsCorrectMessage_WhenRefillingHappens()
        {
            int expectedWaterCapacity = 500;

            CoffeeMat coffeMat = new CoffeeMat(500, 10);

            Assert.AreEqual($"Water tank is filled with {expectedWaterCapacity}ml", coffeMat.FillWaterTank());
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
        public void AddDrink_FailsToAddDrink_WhenThereAreNoAvailableButtonsAndDrinkAlreadyExists()
        {
            string drink = "Coffee";
            double price = 1.50;
            CoffeeMat coffeeMat = new CoffeeMat(500, 1);
            coffeeMat.AddDrink(drink, price);

            bool drinkAdded = coffeeMat.AddDrink("Coffee", 1.50);

            Assert.IsFalse(drinkAdded);
        }



        [Test]
        public void BuyDrink_Fails_WhenThereIsNotEnoughWater()
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, 1);
            coffeeMat.AddDrink("Coffee", 1.50);
            coffeeMat.BuyDrink("Coffee");

            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("Coffee"));
        }

        [Test]
        public void BuyDrink_Fails_WhenThereIsNoSuchDrink()
        {
            string name = "Coffee";
            CoffeeMat coffeeMat = new CoffeeMat(100, 1);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Tea", 1.50);

            Assert.AreEqual($"{name} is not available!", coffeeMat.BuyDrink("Coffee"));
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