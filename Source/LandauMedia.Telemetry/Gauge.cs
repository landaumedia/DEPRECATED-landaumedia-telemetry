using System;
using LandauMedia.Telemetry.Internal;

namespace LandauMedia.Telemetry
{
    public class Gauge : ITelemeter
    {
        readonly Lazy<string> _lazyName;
        Action<Lazy<string>, long> _gauge;

        internal Gauge(LazyName lazyName)
        {
            _lazyName = lazyName.Get(this);
        }

        void ITelemeter.ChangeImplementation(ITelemeterImpl impl)
        {
            _gauge = impl.GetGauge();
        }

        public void Set(long value)
        {
            _gauge(_lazyName, value);
        }
    }
}