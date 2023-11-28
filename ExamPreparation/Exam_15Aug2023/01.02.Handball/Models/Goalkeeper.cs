namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double initialRating = 2.5;
        private const double increaseRating = 0.75;
        private const double decreaseRating = 1.25;

        public Goalkeeper(string name)
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
