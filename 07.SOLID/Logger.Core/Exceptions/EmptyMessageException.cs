namespace Logger.Core.Exceptions
{
    public class EmptyMessageException : Exception
    {
        private const string DefaultMessage = "Message text is empty";

        public EmptyMessageException()
            : base(DefaultMessage)
        {

        }
        public EmptyMessageException(string message)
            : base(message)
        {

        }
    }
}
