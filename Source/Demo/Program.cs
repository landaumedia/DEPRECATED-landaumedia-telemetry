using System;
using System.Threading;
using System.Threading.Tasks;
using LandauMedia.Telemetry;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            Telemeter.Initialize("telemetry.dev.local", 8125, "Development");

            var rand = new Random();
            Task.Run(() =>
            {
                while(true)
                {
                    Telemetry.Foo.Increment();

                    Telemetry.Lol.Record(TimeSpan.FromSeconds(1));

                    Thread.Sleep(rand.Next(500) + 1);
                }
            });
            Task.Run(() =>
            {
                while(true)
                {
                    Telemetry.Master.Bar.Increment();

                    using(Telemetry.Master.Took.Record())
                    {
                        Thread.Sleep(rand.Next(500) + 1);
                    }
                }
            });
            Task.Run(() =>
            {
                while(true)
                {
                    Telemetry.Master.Sub.Muhhaa.Increment();

                    var value = rand.Next(500) + 1;
                    Thread.Sleep(value);

                    Telemetry.Master.Sub.LastMuhhaa.Set(value);
                }
            });
            Console.ReadLine();
        }
    }
}