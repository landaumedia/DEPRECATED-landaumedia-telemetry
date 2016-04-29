# LandauMedia.Telemetry

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/landaumedia/landaumedia-telemetry/master/LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/3ybrkx2ooicwndy6?svg=true)](https://ci.appveyor.com/project/lanwin/landaumedia-telemetry)
[![NuGet](https://img.shields.io/nuget/v/LandauMedia.Telemetry.svg?maxAge=2592000)](https://www.nuget.org/packages/LandauMedia.Telemetry)

Its an tiny opinionated lib, which is able to sends telemetry data in StatsD format and can be used in a strongly typed way (means without spreading the string metricnames all over your code).

## Getting started

#### Define Telemeters

Change the product name of your **AssemblyProduct** attribute in your **AssemblyInfo.cs**.

```csharp
[assembly: AssemblyProduct("Demo")]
```

Put a **Telemetry.cs** into the root of your project. Add a class like the following to it:

```csharp
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
```

#### Initialize

```csharp
// before this line, calls to telemeters do nothing at all
Telemeter.Initialize("statsdhost", 8125, "Testing");
```

#### Use Telemeters

```csharp
Telemetry.Foo.Increment();
Telemetry.Lol.Record(TimeSpan.FromSeconds(1));

using(Telemetry.Master.Took.Record())
{
  Thread.Sleep(1000);
}

Telemetry.Master.Sub.LastMuhhaa.Set(123);
```

#### Metricnames

The generated metricnames have the following format:

     $assemblyname$.$machinename$(.$environment$).$rootpropname$.$sub1name$.$sub1propname$....

Given the machinename is ***MacGyver*** with the above code, we get the following metricnames:

* demo.macgyver.testing.foo
* demo.macgyver.testing.lol
* demo.macgyver.testing.master.bar
* demo.macgyver.testing.master.took
* demo.macgyver.testing.master.sub.lastmuhhaa
* demo.macgyver.testing.master.sub.muhhaa

### FAQ

#### Uhhmm Static really? What about testing?
As long as you dont call Initialize in your Testsetup, nothing will happen.

#### Whats the overhead?
The most overhead is generated when you call the static class the first time. There there will be StackFrames called to resolve the metricnames.

**Without calling Initialize** you only pay for for a method call. Look for [EmptyTelemeterImpl.cs](https://github.com/landaumedia/landaumedia-telemetry/blob/master/Source/LandauMedia.Telemetry/Internal/EmptyTelemeterImpl.cs).

**When Initialized** you pay basically only for a method call and sending the UDP package. While the package data is cache and created only once whenever possible.
