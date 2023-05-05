using Customer.Service.EventHandlers.Commands;
using Customer.Service.Queries;
using Customer.Service.Queries.DTO;
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

namespace Customer.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("v1/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IMediator _mediator;
        private readonly IClientQueryService _clientQueryService;
        public ClientController(
            ILogger<ClientController> logger,
            IMediator mediator,
            IClientQueryService clientQueryService) {
            _logger = logger;
            _mediator = mediator;
            _clientQueryService = clientQueryService;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(
            int page = 1,
            int take = 10,
            string ids = null)
        {
            IEnumerable<int> clients = null;

            if (!string.IsNullOrEmpty(ids))
            {
                clients = ids.Split(',').Select(x => Convert.ToInt32(x));
            }

            return await _clientQueryService.GetAllAsync(page, take, clients);
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await _clientQueryService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
