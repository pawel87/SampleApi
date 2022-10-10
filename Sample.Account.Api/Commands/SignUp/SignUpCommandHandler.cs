using MediatR;
using Microsoft.AspNetCore.Identity;
using Sample.Account.Api.Exceptions;
using System.Net;

namespace Sample.Account.Api.Commands.SignUp
{
    public class RegisterCommandHandler : IRequestHandler<SignUpCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var userAlreadyExists = await _userManager.FindByNameAsync(request?.Model?.Username);
            if(userAlreadyExists == null)
            {
                var user = new IdentityUser()
                {
                    Email = request?.Model?.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = request?.Model?.Username
                };

                var result = await _userManager.CreateAsync(user, request?.Model?.Password);
                if (result.Succeeded)
                    return true;
            }

            return false;
        }

        private static void Validate(SignUpCommand command)
        {
            if(command == null || command.Model == null)
                throw new ApiException("Model is required", HttpStatusCode.BadRequest);
        }
    }
}
