using System.Web.Http;
using LandauMedia.Telemetry;
using Owin;

namespace DemoWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Telemeter.Initialize("telemetry.dev.local", 8125, "Development");

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MessageHandlers.Add(new TrackRequests());
            WebApiConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}