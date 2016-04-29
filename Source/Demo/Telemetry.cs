using LandauMedia.Telemetry;

namespace Demo
{
    static class Telemetry
    {
        public static readonly Counter Foo = Telemeter.Counter();

        public static readonly Timing Lol = Telemeter.Timing();

        public static class Master
        {
            public static readonly Counter Bar = Telemeter.Counter();
            public static readonly Timing Took = Telemeter.Timing();

            public static class Sub
            {
                public static readonly Gauge LastMuhhaa = Telemeter.Gauge();
                public static readonly Counter Muhhaa = Telemeter.Counter();
            }
        }
    }
}