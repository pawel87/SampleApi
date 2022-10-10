using MediatR;
using Sample.Account.Api.Models;
using Sample.Account.Api.ViewModels;

namespace Sample.Account.Api.Commands.SignIn
{
    public class SignInCommand : IRequest<JwtTokenViewModel?>
    {
        public SignInModel? Model { get; set; }
    }
}
