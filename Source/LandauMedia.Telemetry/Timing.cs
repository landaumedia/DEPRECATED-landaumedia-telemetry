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

        public IDisposable Record(string postfix = null)
        {
            var watch = Stopwatch.StartNew();
            return new ActionDisposable(() => _timing(NameWithPostfix(_lazyName, postfix), watch.ElapsedMilliseconds));
        }

        public void Record(Stopwatch watch, string postfix = null)
        {
            _timing(NameWithPostfix(_lazyName, postfix), watch.ElapsedMilliseconds);
        }

        public void Record(TimeSpan timespan, string postfix = null)
        {
            _timing(NameWithPostfix(_lazyName, postfix), (long)timespan.TotalMilliseconds);
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