using NavalVessels;
using System.Reflection;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

[TestFixture]
public class Tests_015
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ServiceVesselReturnNotFound()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var submarine = new object[] { "U-401", "Submarine", 17, 22 };
        InvokeMethod(controller, "ProduceVessel", submarine);

        var battleship = new object[] { "USS_Nevada", "Battleship", 220, 19 };
        InvokeMethod(controller, "ProduceVessel", battleship).ToString().Trim();

        var addCaptainArguments = new object[] { "Ernest_King" };
        InvokeMethod(controller, "HireCaptain", addCaptainArguments);
        var addCaptainArgumentsTwo = new object[] { "Isoroku_Yamamoto" };
        InvokeMethod(controller, "HireCaptain", addCaptainArgumentsTwo);

        var assignTest = new object[] { "Ernest_King", "USS_Nevada" };
        InvokeMethod(controller, "AssignCaptain", assignTest);

        var assignTestTwo = new object[] { "Isoroku_Yamamoto", "U-401" };
        InvokeMethod(controller, "AssignCaptain", assignTestTwo);

        var attackObj = new object[] { "USS_Nevada", "U-401" };
        var result = InvokeMethod(controller, "AttackVessels", attackObj);
        var actualResult = "Vessel U-401 was attacked by vessel USS_Nevada - current armor thickness: 0.";

        var attackUnarmoredVessel = InvokeMethod(controller, "AttackVessels", new object[] { "USS_Nevada", "U-401" });
        var actualResultUnarmoredVessel = "Unarmored vessel U-401 cannot attack or be attacked.";

        var unexistingVesselAttackerOne = InvokeMethod(controller, "AttackVessels", new object[] { "USS_None", "U-401" });
        var actualResultunexistingVesselAttackerOne = "Vessel USS_None could not be found.";

        var unexistingVesselAttackerTwo = InvokeMethod(controller, "AttackVessels", new object[] { "USS_Nevada", "U-0" });
        var actualResultunexistingVesselAttackerTwo = "Vessel U-0 could not be found.";

        Assert.AreEqual(result, actualResult);
        Assert.AreEqual(attackUnarmoredVessel, actualResultUnarmoredVessel);
        Assert.AreEqual(unexistingVesselAttackerOne, actualResultunexistingVesselAttackerOne);
        Assert.AreEqual(unexistingVesselAttackerTwo, actualResultunexistingVesselAttackerTwo);
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
                object currentInstance;
                if (parameterInfo.ParameterType.Name.StartsWith("I"))
                {
                    currentInstance = Activator.CreateInstance(GetType(parameterInfo.ParameterType.Name.Substring(1)));
                }
                else
                {
                    currentInstance = Activator.CreateInstance(GetType(parameterInfo.ParameterType.Name));
                }

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            throw e.InnerException;
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == name);

        return type;
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        var result = obj.GetType()
            .GetMethod(methodName)
            .Invoke(obj, parameters);

        return result;
    }

    private static string RemoveNewLines(string str)
    {
        var newLineSymbols = new[]
        {
            "\r\n",
            "\n"
        };

        var replacement = string.Empty;

        foreach (var newLineSymbol in newLineSymbols)
        {
            str = str.Replace(newLineSymbol, replacement);
        }

        return str;
    }
}