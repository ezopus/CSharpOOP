namespace Logger.Core.Exceptions
{
    public class InvalidDateTimeFormatException : Exception
    {
        private const string DefaultMessage = "Invalid date/time format!";

        public InvalidDateTimeFormatException()
            : base(DefaultMessage)
        {

        }
        public InvalidDateTimeFormatException(string message)
            : base(message)
        {

        }
    }
}
