using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.Queries;
using Catalog.Service.Queries.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Common.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("v1/stocks")]
    public class ProductInStockController : ControllerBase
    {
        private readonly ILogger<ProductInStockController> _logger;
        private readonly IMediator _mediator;

        public ProductInStockController(
            ILogger<ProductInStockController> logger,
            IMediator mediator
            )
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStock(ProductInStockUpdateCommand command)
        {
            await _mediator.Publish(command);
            return NoContent();
        }
    }
}
