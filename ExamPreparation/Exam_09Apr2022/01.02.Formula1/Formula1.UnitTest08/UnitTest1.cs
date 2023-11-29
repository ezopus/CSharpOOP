using Formula1;
using System.Reflection;

public class Tests_008
{
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void CreatePilotValidationPropertiesThrowsException()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var pilotArgument = new object[] { "Fer" };
        var pilotArgumentTwo = new object[] { "" };
        var validActualResultOne = InvokeMethod(controller, "CreatePilot", pilotArgument);
        var validActualResultTwo = InvokeMethod(controller, "CreatePilot", pilotArgumentTwo);

        var validExpectedResult = "Invalid pilot name: Fer.";
        var validExpectedResultTwo = "Invalid pilot name: .";


        Assert.AreEqual(validExpectedResult, validActualResultOne);
        Assert.AreEqual(validExpectedResultTwo, validActualResultTwo);
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
