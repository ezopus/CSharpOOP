namespace Logger.Core.Exceptions
{
    public class EmptyCreateTimeException : Exception
    {
        private const string DefaultMessage = "Date and time is empty";

        public EmptyCreateTimeException()
            : base(DefaultMessage)
        {

        }
        public EmptyCreateTimeException(string message)
            : base(message)
        {

        }
    }
}
