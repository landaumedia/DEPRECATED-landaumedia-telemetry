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

        public void Set(long value, string postfix = null)
        {
            _gauge(NameWithPostfix(_lazyName, postfix), value);
        }

        public void Set(long? value, string postfix = null)
        {
            if (value.HasValue)
                _gauge(NameWithPostfix(_lazyName, postfix), value.Value);
        }

        static Lazy<string> NameWithPostfix(Lazy<string> baseName, string postfix = null)
        {
            var name = baseName;

            // erweitern um 
            if (postfix != null)
                name = new Lazy<string>(() => baseName.Value + "." + postfix);

            return name;

        }
    }
}