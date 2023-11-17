namespace Logger.Core.Exceptions
{
    public class EmptyFileExtension : Exception
    {
        private const string DefaultMessage = "Empty file extension";

        public EmptyFileExtension()
            : base(DefaultMessage)
        {

        }
        public EmptyFileExtension(string message)
            : base(message)
        {

        }
    }
}
