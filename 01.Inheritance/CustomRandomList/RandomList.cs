namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        Random random = new Random();

        public string RandomString()
        {
            string removed = this[random.Next(0, this.Count)];
            this.Remove(removed);
            return removed;
        }
    }
}
