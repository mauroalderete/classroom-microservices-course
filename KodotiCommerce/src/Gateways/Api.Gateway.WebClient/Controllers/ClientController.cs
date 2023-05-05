using Api.Gateway.Models.Collection;
using Api.Gateway.Models.Customer.Commands;
using Api.Gateway.Models.Customer.DTO;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly ICustomerProxy _customerProxy;

        public ClientController(ILogger<DefaultController> logger, ICustomerProxy customerProxy)
        {
            _logger = logger;
            _customerProxy = customerProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            return await _customerProxy.GetAll(page, take, ids);
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await _customerProxy.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand command)
        {
            await _customerProxy.Create(command);
            return Ok();
        }
    }
}
