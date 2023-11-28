namespace Handball.Models
{
    public class CenterBack : Player
    {
        private const double initialRating = 4;
        private const double increaseRating = 1;
        private const double decreaseRating = 1;

        public CenterBack(string name)
            : base(name, initialRating)
        {

        }
        public override void IncreaseRating()
        {
            if (Rating + increaseRating > 10)
            {
                Rating = 10;
            }
            else
            {
                Rating += increaseRating;
            }
        }

        public override void DecreaseRating()
        {
            if (Rating - decreaseRating < 1)
            {
                Rating = 1;
            }
            else
            {
                Rating -= decreaseRating;
            }
        }

    }
}
