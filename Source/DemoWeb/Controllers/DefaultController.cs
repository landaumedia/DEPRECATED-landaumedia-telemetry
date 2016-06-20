using System.Web.Http;

namespace DemoWeb.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            

            return "Hello world";
        }
    }
}
