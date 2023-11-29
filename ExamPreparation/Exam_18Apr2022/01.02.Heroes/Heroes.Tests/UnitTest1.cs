using Heroes;
using System.Reflection;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_019
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void StartBattleWorksProperly()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var createHeroArgumentOne = new object[] { "Knight", "Wilmetta", 50, 20 };
        var createHeroArgumentTwo = new object[] { "Knight", "ldis", 10, 35 };
        var createHeroArgumentThree = new object[] { "Barbarian", "Dave", 35, 70 };
        var createHeroArgumentFour = new object[] { "Barbarian", "Casey", 35, 1 };
        var createHeroArgumentFive = new object[] { "Barbarian", "Attila", 0, 1 };

        var createWeaponArgumentOne = new object[] { "Claymore", "Almace", 12 };
        var createWeaponArgumentTwo = new object[] { "Mace", "Caliburn", 12 };
        var createWeaponArgumentThree = new object[] { "Claymore", "Durandal", 12 };
        var createWeaponArgumentFour = new object[] { "Claymore", "Narcoleptic", 12 };
        var createWeaponArgumentFive = new object[] { "Claymore", "Stormcaller", 12 };

        var addWeaponToHeroArgumentsOne = new object[] { "Almace", "Wilmetta" };
        var addWeaponToHeroArgumentsTwo = new object[] { "Caliburn", "Dave" };
        var addWeaponToHeroArgumentsThree = new object[] { "Durandal", "ldis" };
        var addWeaponToHeroArgumentsFour = new object[] { "Narcoleptic", "Casey" };
        var addWeaponToHeroArgumentsFive = new object[] { "Stormcaller", "Attila" };

        InvokeMethod(controller, "CreateHero", createHeroArgumentOne);
        InvokeMethod(controller, "CreateHero", createHeroArgumentTwo);
        InvokeMethod(controller, "CreateHero", createHeroArgumentThree);
        InvokeMethod(controller, "CreateHero", createHeroArgumentFour);
        InvokeMethod(controller, "CreateHero", createHeroArgumentFive);

        InvokeMethod(controller, "CreateWeapon", createWeaponArgumentOne);
        InvokeMethod(controller, "CreateWeapon", createWeaponArgumentTwo);
        InvokeMethod(controller, "CreateWeapon", createWeaponArgumentThree);
        InvokeMethod(controller, "CreateWeapon", createWeaponArgumentFour);
        InvokeMethod(controller, "CreateWeapon", createWeaponArgumentFive);

        InvokeMethod(controller, "AddWeaponToHero", addWeaponToHeroArgumentsOne);
        InvokeMethod(controller, "AddWeaponToHero", addWeaponToHeroArgumentsTwo);
        InvokeMethod(controller, "AddWeaponToHero", addWeaponToHeroArgumentsThree);
        InvokeMethod(controller, "AddWeaponToHero", addWeaponToHeroArgumentsFour);
        InvokeMethod(controller, "AddWeaponToHero", addWeaponToHeroArgumentsFive);

        var result = InvokeMethod(controller, "StartBattle", null);
        var expectedResult = "The barbarians took 1 casualties but won the battle.";

        Assert.AreEqual(expectedResult, result);
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