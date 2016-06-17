using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using LandauMedia.Telemetry.Internal;

namespace LandauMedia.Telemetry
{
    public static class Telemeter
    {
        static readonly List<ITelemeter> Telemeters = new List<ITelemeter>();
        static string _baseName;
        static ITelemeterImpl _impl = new EmptyTelemeterImpl();

        public static bool ThrowWhenInitializeFailed { get; set; }

        public static void Initialize(string host, int port, string environment)
        {
            if(!( _impl is EmptyTelemeterImpl ))
                throw new InvalidOperationException("Already initialized");

            try
            {
                var entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
                var baseName = entryAssembly.GetCustomAttributes(true)
                    .OfType<AssemblyTitleAttribute>()
                    .Select(t => t.Title)
                    .DefaultIfEmpty(Path.GetFileNameWithoutExtension(entryAssembly.Location))
                    .FirstOrDefault();

                baseName += "." + Environment.MachineName;

                if(environment != null)
                    baseName += "." + environment;

                _baseName = baseName;

                _impl = new UdpTelemeterImpl(host, port);
                Telemeters.ForEach(stat => stat.ChangeImplementation(_impl));
            }
            catch(Exception exception)
            {
                Trace.WriteLine("Failed to initialize telemeter: " + exception);
                if(ThrowWhenInitializeFailed)
                    throw;
            }
        }

        public static Counter Counter()
        {
            var lazyName = CreateLazyName();
            return Add(new Counter(lazyName));
        }

        public static Gauge Gauge()
        {
            var lazyName = CreateLazyName();
            return Add(new Gauge(lazyName));
        }

        public static Timing Timing()
        {
            var lazyName = CreateLazyName();
            return Add(new Timing(lazyName));
        }

        static T Add<T>(T metric) where T : ITelemeter
        {
            metric.ChangeImplementation(_impl);
            Telemeters.Add(metric);
            return metric;
        }

        static LazyName CreateLazyName()
        {
            // yes, but we have that cost only once
            var baseType = new StackFrame(2, true).GetMethod().DeclaringType;
            var lazyName = new LazyName(() => _baseName, baseType);
            return lazyName;
        }
    }
}