using Library_project.Data;
using Library_project.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Library_project.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly TokenSettings _tokensettings;
        public AccountService(AppDbContext dbContext, IOptions<TokenSettings> tokensettings)
        {
            _dbContext = dbContext;
            _tokensettings = tokensettings.Value;
        }
    }
    
}
