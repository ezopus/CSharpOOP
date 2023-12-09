namespace NauticalCatchChallenge.Models.FishModels
{
    public class ReefFish : Fish

    {
        private const int ReefFishTimeToCatch = 30;
        public ReefFish(string name, double points)
            : base(name, points, ReefFishTimeToCatch)
        {
        }
    }
}
