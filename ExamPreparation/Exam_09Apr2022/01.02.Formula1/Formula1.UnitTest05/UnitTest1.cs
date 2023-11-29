using Formula1;
using System.Reflection;

public class Tests_005
{
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void TestFormulaOneCarValidationProperties()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var carArgumentsOne = new object[] { "Ferrari", "FW", 971, 1.6 };
        var carArgumentsTwo = new object[] { "Ferrari", null, 972, 1.7 };
        var carArgumentsThree = new object[] { "Williams", "FW14", 850, 1.7 };
        var carArgumentsFour = new object[] { "Williams", "FW123", 1100, 1.7 };
        var carArgumentsFive = new object[] { "Ferrari", "FW565", 970, 1.5 };
        var carArgumentsSix = new object[] { "Ferrari", "FW124", 970, 2.1 };
        var resultOne = InvokeMethod(controller, "CreateCar", carArgumentsOne);
        var resultTwo = InvokeMethod(controller, "CreateCar", carArgumentsTwo);
        var resultThree = InvokeMethod(controller, "CreateCar", carArgumentsThree);
        var resultFour = InvokeMethod(controller, "CreateCar", carArgumentsFour);
        var resultFive = InvokeMethod(controller, "CreateCar", carArgumentsFive);
        var resultSix = InvokeMethod(controller, "CreateCar", carArgumentsSix);

        var validExpectedResultOne = "Invalid car model: FW.";
        var validExpectedResultTwo = "Invalid car model: .";
        var validExpectedResultThree = "Invalid car horsepower: 850.";
        var validExpectedResultFour = "Invalid car horsepower: 1100.";
        var validExpectedResultFive = "Invalid car engine displacement: 1.5.";
        var validExpectedResultSix = "Invalid car engine displacement: 2.1.";

        Assert.AreEqual(resultOne, validExpectedResultOne);
        Assert.AreEqual(resultTwo, validExpectedResultTwo);
        Assert.AreEqual(resultThree, validExpectedResultThree);
        Assert.AreEqual(resultFour, validExpectedResultFour);
        Assert.AreEqual(resultFive, validExpectedResultFive);
        Assert.AreEqual(resultSix, validExpectedResultSix);
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        try
        {
            var result = obj.GetType()
                .GetMethod(methodName)
                .Invoke(obj, parameters);

            return result;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static object CreateObjectInstance(Type type, params object[] parameters)
    {
        try
        {
            var desiredConstructor = type.GetConstructors()
                .FirstOrDefault(x => x.GetParameters().Any());

            if (desiredConstructor == null)
            {
                return Activator.CreateInstance(type, parameters);
            }

            var instances = new List<object>();

            foreach (var parameterInfo in desiredConstructor.GetParameters())
            {
                var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name.Contains(name));

        return type;
    }
}