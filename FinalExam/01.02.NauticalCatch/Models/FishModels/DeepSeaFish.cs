namespace NauticalCatchChallenge.Models.FishModels
{
    public class DeepSeaFish : Fish
    {
        private const int DeepSeaFishTimeToCatch = 180;
        public DeepSeaFish(string name, double points)
            : base(name, points, DeepSeaFishTimeToCatch)
        {
        }
    }
}
