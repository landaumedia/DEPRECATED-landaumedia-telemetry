using System;
using System.Threading;

namespace LandauMedia.Telemetry.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Telemeter.Initialize("telemetry.dev.local", 8125, "staging");

            while (true)
            {
                Telemetry.Sample.CountSample.Increment("api-1");    
                Telemetry.Sample.CountSample.Increment("api-2");    
                Thread.Sleep(2000);
                Console.WriteLine("Ping");
            }
           
            Console.WriteLine("finish");
            Console.ReadLine();
        }
    }
}
