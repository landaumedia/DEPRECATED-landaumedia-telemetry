using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        public void Increment(string postfix = null)
        {
            _increamentByOne(NameWithPostfix(_lazyName, postfix));
        }

        public void Increment(int count, string postfix = null)
        {
            if (count == 0)
                return;

            _icnremnent(NameWithPostfix(_lazyName, postfix), count);
        }

        public void Decrement(string postfix = null)
        {
            _decrementByOne(NameWithPostfix(_lazyName, postfix));
        }

        public void Decrement(int count, string postfix = null)
        {
            if (count == 0)
                return;
            _decrement(NameWithPostfix(_lazyName, postfix), count);
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