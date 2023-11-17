using System.Globalization;

namespace Logger.Core.Utils
{
    public static class DateTimeValidator
    {
        private static readonly ISet<string> formats =
            new HashSet<string> { "M/dd/yyyy h:mm:ss tt" };
        public static bool ValidateDateTime(string dateTime)
        {
            foreach (var format in formats)
            {
                //3/31/2015 5:23:54 PM
                if (DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out DateTime result))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
