
namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            if (Count == 0 || this == null)
            {
                return true;
            }
            return false;
        }

        public Stack<string> AddRange()
        {
            return this.AddRange();
        }

    }
}
