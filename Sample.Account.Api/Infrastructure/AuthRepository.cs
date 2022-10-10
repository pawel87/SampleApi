using Microsoft.EntityFrameworkCore;

namespace Sample.Account.Api.Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountUsers()
        {
            var result = await _context.IdentityUsers.CountAsync();
            return result;
        }
    }
}
