using Identity.Domain;
using Identity.Service.EventHandler.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Service.EventHandler
{
    public class UserCreateEventHandler : IRequestHandler<UserCreateCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCreateEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            return result;
        }
    }
}
