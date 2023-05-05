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
        private readonly ICustomerProxy _customerProxy;
        private readonly ICatalogProxy _catalogProxy;

        public OderController(
            ILogger<DefaultController> logger,
            IOrderProxy orderProxy,
            ICustomerProxy customerProxy,
            ICatalogProxy catalogProxy)
        {
            _logger = logger;
            _orderProxy = orderProxy;
            _customerProxy = customerProxy;
            _catalogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            return await _orderProxy.GetOrdersAsync();
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var order = await _orderProxy.GetOrderAsync(id);

            order.Client = await _customerProxy.Get(order.ClientId);

            var productsId = order.Items.Select(x => x.ProductId).Distinct().ToList();
            var productsIdarg = string.Join(",", productsId);

            var products = await _catalogProxy.GetAll(1, productsId.Count,  productsIdarg);

            foreach( var item in order.Items)
            {
                item.Product = products.Items.Single(p => p.ProductId == item.ProductId);
            }

            return order;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateOrderAsync(command);
            return Ok();
        }
    }
}
