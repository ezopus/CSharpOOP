namespace Logger.Core.Exceptions
{
    public class EmptyFileName : Exception
    {
        private const string DefaultMessage = "Empty file name";

        public EmptyFileName()
            : base(DefaultMessage)
        {

        }
        public EmptyFileName(string message)
            : base(message)
        {

        }
    }
}
