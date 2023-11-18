using System.Reflection;
using System.Text;

namespace AuthorProblem
{
    public class Tracker
    {
        public void PrintMethodsByAuthor()
        {
            StringBuilder sb = new StringBuilder();
            Type classType = typeof(StartUp);
            MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            foreach (var method in methods)
            {
                if (method.CustomAttributes.Any(n => n.AttributeType == typeof(AuthorAttribute)))
                {
                    var attributes = method.GetCustomAttributes(false);
                    foreach (AuthorAttribute attribute in attributes)
                    {
                        sb.AppendLine($"{method.Name} is written by {attribute.Name}");
                    }
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
