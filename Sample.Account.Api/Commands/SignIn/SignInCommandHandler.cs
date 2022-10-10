using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Sample.Account.Api.Exceptions;
using Sample.Account.Api.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Sample.Account.Api.Commands.SignIn
{
    public class AuthenticateCommandHandler : IRequestHandler<SignInCommand, JwtTokenViewModel?>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<JwtTokenViewModel?> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var user = await _userManager.FindByNameAsync(request?.Model?.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, request?.Model?.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = GetToken(claims);

                return new JwtTokenViewModel()
                {
                    ExpiresAt = token.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }

            return null;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static void Validate(SignInCommand command)
        {
            if(command == null || command.Model == null)
                throw new ApiException("Model is required", HttpStatusCode.BadRequest);
        }
    }
}
