using System;
using LandauMedia.Telemetry.Internal;

namespace LandauMedia.Telemetry
{
    public class Counter : ITelemeter
    {
        readonly Lazy<string> _lazyName;
        Action<Lazy<string>, int> _decrement;
        Action<Lazy<string>> _decrementByOne;
        Action<Lazy<string>, int> _icnremnent;
        Action<Lazy<string>> _increamentByOne;

        internal Counter(LazyName lazyName)
        {
            _lazyName = lazyName.Get(this);
        }

        void ITelemeter.ChangeImplementation(ITelemeterImpl impl)
        {
            _increamentByOne = impl.GetCounterIncrementByOne();
            _icnremnent = impl.GetCounterIncrement();
            _decrement = impl.GetCounterDecrement();
            _decrementByOne = impl.GetCounterDecrementByOne();
        }

        public void Increment()
        {
            _increamentByOne(_lazyName);
        }

        public void Increment(int count)
        {
            _icnremnent(_lazyName, count);
        }

        public void Decrement()
        {
            _decrementByOne(_lazyName);
        }

        public void Decrement(int count)
        {
            _decrement(_lazyName, count);
        }
    }
}