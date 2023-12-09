namespace NauticalCatchChallenge.Models.FishModels
{
    public class PredatoryFish : Fish
    {
        private const int PredatoryFishTimeToCatch = 60;
        public PredatoryFish(string name, double points)
            : base(name, points, PredatoryFishTimeToCatch)
        {
        }
    }
}
