using Logger.Core.Layout.Interfaces;

namespace Logger.Core.Layout
{
    public class SimpleLayout : ILayout
    {
        private const string SimpleFormat = "{0} - {1} - {2}";
        public string Format => SimpleFormat;
    }
}
