using MediatR;
using Sample.Account.Api.Infrastructure;
using Sample.Account.Api.ViewModels;

namespace Sample.Account.Api.Queries.GetAmountOfRegisteredUsers
{
    public class GetAmountOfRegisteredUsersQueryHandler : IRequestHandler<GetAmountOfRegisteredUsersQuery, AmountOfRegisteredUsersViewModel>
    {
        private IAuthRepository _repository;

        public GetAmountOfRegisteredUsersQueryHandler(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<AmountOfRegisteredUsersViewModel> Handle(GetAmountOfRegisteredUsersQuery request, CancellationToken cancellationToken)
        {
            var amountOfUsers = await _repository.CountUsers();

            return new AmountOfRegisteredUsersViewModel()
            {
                Amount = amountOfUsers
            };
        }
    }
}
