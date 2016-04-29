using System;

namespace LandauMedia.Telemetry.Internal
{
    /// <summary>
    /// It does nothing!
    /// </summary>
    class EmptyTelemeterImpl : ITelemeterImpl
    {
        public Action<Lazy<string>> GetCounterIncrementByOne()
        {
            return delegate { };
        }

        public Action<Lazy<string>, int> GetCounterIncrement()
        {
            return delegate { };
        }

        public Action<Lazy<string>> GetCounterDecrementByOne()
        {
            return delegate { };
        }

        public Action<Lazy<string>, int> GetCounterDecrement()
        {
            return delegate { };
        }

        public Action<Lazy<string>, long> GetTiming()
        {
            return delegate { };
        }

        public Action<Lazy<string>, long> GetGauge()
        {
            return delegate { };
        }
    }
}