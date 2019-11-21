namespace LandauMedia.Telemetry.Playground
{
    public static class Telemetry
    {
        public static class Sample
        {
            public static readonly Timing Duration = Telemeter.Timing();
            public static readonly Counter CountSample = Telemeter.Counter();
        }
    }
}