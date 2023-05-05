using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using Catalog.Service.Queries.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductQueryService _productQueryService;
        private readonly IMediator _mediator;

        public ProductController(
            ILogger<ProductController> logger,
            IProductQueryService productQuery,
            IMediator mediator
            )
        {
            _logger = logger;
            _productQueryService = productQuery;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            IEnumerable<int> products = null;

            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await _productQueryService.GetAllAsync(page, take, products);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            return await _productQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            await _mediator.Publish(command);

            return Ok();
        }
    }
}
