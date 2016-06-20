using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWeb
{
    class TrackRequests : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using(Telemetry.Request.Took.Record())
            {
                Telemetry.Request.Incoming.Increment();

                try
                {
                    return await base.SendAsync(request, cancellationToken);
                }
                catch(Exception)
                {
                    Telemetry.Request.Failed.Increment();
                    throw;
                }
            }
        }
    }
}