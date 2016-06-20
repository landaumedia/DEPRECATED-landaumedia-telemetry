using LandauMedia.Telemetry;

namespace DemoWeb
{
    static class Telemetry
    {
        public static class Request
        {
            public static readonly Counter Incoming = Telemeter.Counter();
            public static readonly Counter Failed = Telemeter.Counter();
            public static readonly Timing Took = Telemeter.Timing();
        }
    }
}