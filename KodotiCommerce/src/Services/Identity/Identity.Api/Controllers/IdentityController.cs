using Identity.Domain;
using Identity.Service.EventHandler.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("v1/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityController(
                       IMediator mediator,
                                  ILogger<IdentityController> logger,
                                             SignInManager<ApplicationUser> signInManager)
        {
            _mediator = mediator;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                else
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login(UserLoginCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);
                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
