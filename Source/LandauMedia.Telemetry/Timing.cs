using System;
using System.Diagnostics;
using LandauMedia.Telemetry.Internal;

namespace LandauMedia.Telemetry
{
    public class Timing : ITelemeter
    {
        readonly Lazy<string> _lazyName;
        Action<Lazy<string>, long> _timing;

        internal Timing(LazyName lazyName)
        {
            _lazyName = lazyName.Get(this);
        }

        void ITelemeter.ChangeImplementation(ITelemeterImpl impl)
        {
            _timing = impl.GetTiming();
        }

        public IDisposable Record()
        {
            var watch = Stopwatch.StartNew();
            return new ActionDisposable(() => _timing(_lazyName, watch.ElapsedMilliseconds));
        }

        public void Record(Stopwatch watch)
        {
            _timing(_lazyName, watch.ElapsedMilliseconds);
        }

        public void Record(TimeSpan timespan)
        {
            _timing(_lazyName, (long)timespan.TotalMilliseconds);
        }

        class ActionDisposable : IDisposable
        {
            readonly Action _action;

            public ActionDisposable(Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                _action();
            }
        }
    }
}