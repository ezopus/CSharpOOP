using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldNames)
        {
            Type classType = Type.GetType(className);
            FieldInfo[] fields = classType.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            StringBuilder sb = new();

            sb.AppendLine($"Class under investigation: {classType.FullName}");

            foreach (FieldInfo field in fields.Where(f => fieldNames.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().Trim();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            Type classType = Type.GetType(className);

            FieldInfo[] fields = classType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            MethodInfo[] publicMethods = classType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] nonPublicMethod = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new();

            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (var method in publicMethods.Where(m => m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} have to be public!");
            }

            foreach (var method in nonPublicMethod.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} have to be private!");
            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            Type classType = Type.GetType(className);
            MethodInfo[] privateMethods = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new();
            sb.AppendLine($"All Private Methods of Class: {classType.FullName}");
            sb.AppendLine($"Base Class: {classType.BaseType.Name}");
            foreach (var method in privateMethods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public string CollectGettersAndSetters(string className)
        {
            Type classType = Type.GetType(className);

            MethodInfo[] fieldsPrivate = classType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            StringBuilder sb = new();

            foreach (var field in fieldsPrivate.Where(f => f.Name.StartsWith("get")))
            {
                sb.AppendLine($"{field.Name} will return {field.ReturnType}");
            }

            foreach (var field in fieldsPrivate.Where(f => f.Name.StartsWith("set")))
            {
                sb.AppendLine($"{field.Name} will set field of {field.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
