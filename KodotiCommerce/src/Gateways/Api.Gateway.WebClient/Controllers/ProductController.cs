using Api.Gateway.Models.Catalog.Commands;
using Api.Gateway.Models.Catalog.DTO;
using Api.Gateway.Models.Collection;
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
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;
        private readonly ICatalogProxy _catalogProxy;

        public ProductController(ILogger<DefaultController> logger, ICatalogProxy catalogProxy)
        {
            _logger = logger;
            _catalogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            return await _catalogProxy.GetAll(page, take, ids);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            return await _catalogProxy.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await _catalogProxy.Create(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateCommand command)
        {
            await _catalogProxy.UpdateStockAsync(command);
            return NoContent();
        }
    }
}
