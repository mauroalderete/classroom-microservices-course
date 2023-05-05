using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [ApiController]
    [Route("v1/order")]
    public class OderController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public OderController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }
    }
}
