using MediatR;
using Sample.Account.Api.Models;

namespace Sample.Account.Api.Commands.SignUp
{
    public class SignUpCommand : IRequest<bool>
    {
        public SignUpModel? Model { get; set; }
    }
}
