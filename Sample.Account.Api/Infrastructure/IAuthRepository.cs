namespace Sample.Account.Api.Infrastructure
{
    public interface IAuthRepository
    {
        Task<int> CountUsers();
    }
}
