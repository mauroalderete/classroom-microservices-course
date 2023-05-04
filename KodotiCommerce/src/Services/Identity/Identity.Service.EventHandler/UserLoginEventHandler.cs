using Identity.Domain;
using Identity.Persistence.Database;
using Identity.Service.EventHandler.Commands;
using Identity.Service.EventHandler.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.Service.EventHandler
{
    public class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserLoginEventHandler(SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();
            var aaa = await _context.Users.ToListAsync();
            var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);
            var response = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (response.Succeeded)
            {
                result.Success = true;
                await GenerateToken(user, result);
            }

            return result;
        }

        private async Task GenerateToken(ApplicationUser user, IdentityAccess identityAccess)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.UTF8.GetBytes(secretKey);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roles = await _context.Roles.Where(r => r.UserRoles.Any(y => y.UserId == user.Id)).ToListAsync();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identityAccess.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}
