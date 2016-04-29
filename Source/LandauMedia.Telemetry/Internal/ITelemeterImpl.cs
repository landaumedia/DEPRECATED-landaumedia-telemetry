using System;

namespace LandauMedia.Telemetry.Internal
{
    interface ITelemeterImpl
    {
        Action<Lazy<string>> GetCounterIncrementByOne();
        Action<Lazy<string>, int> GetCounterIncrement();
        Action<Lazy<string>> GetCounterDecrementByOne();
        Action<Lazy<string>, int> GetCounterDecrement();
        Action<Lazy<string>, long> GetTiming();
        Action<Lazy<string>, long> GetGauge();
    }
}