using Api.Gateway.Models.Collection;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTO;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/orders")]
    public class OderController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly IOrderProxy _orderProxy;

        public OderController(ILogger<DefaultController> logger, IOrderProxy orderProxy)
        {
            _logger = logger;
            _orderProxy = orderProxy;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            return await _orderProxy.GetOrdersAsync();
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            return await _orderProxy.GetOrderAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateOrderAsync(command);
            return Ok();
        }
    }
}
