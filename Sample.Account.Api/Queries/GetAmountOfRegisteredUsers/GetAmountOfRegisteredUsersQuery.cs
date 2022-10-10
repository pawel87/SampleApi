using MediatR;
using Sample.Account.Api.ViewModels;

namespace Sample.Account.Api.Queries.GetAmountOfRegisteredUsers
{
    public class GetAmountOfRegisteredUsersQuery : IRequest<AmountOfRegisteredUsersViewModel>
    {
    }
}
