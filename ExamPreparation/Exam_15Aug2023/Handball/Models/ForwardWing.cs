namespace Handball.Models
{
    public class ForwardWing : Player
    {
        private const double initialRating = 5.5;
        private const double increaseRating = 1.25;
        private const double decreaseRating = 0.75;

        public ForwardWing(string name)
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
