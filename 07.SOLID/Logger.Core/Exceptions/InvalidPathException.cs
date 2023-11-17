namespace Logger.Core.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string DefaultMessage = "Path is invalid";

        public InvalidPathException()
            : base(DefaultMessage)
        {

        }
        public InvalidPathException(string message)
            : base(message)
        {

        }
    }
}
