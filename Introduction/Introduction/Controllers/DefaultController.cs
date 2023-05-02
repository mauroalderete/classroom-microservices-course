using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Controllers
{
    [Route("/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index() {
            return "Running...";
        }
    }
}
