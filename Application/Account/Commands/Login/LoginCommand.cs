using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Account;
using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.Login
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IAccountApiService _authApiService;
        private readonly ITokenRepository _tokenRepository;

        public LoginCommand(IAccountApiService authApiService, ITokenRepository tokenRepository)
        {
            _authApiService = authApiService;
            _tokenRepository = tokenRepository;
        }

        public async Task Execute(string email, string password)
        {
            var loginCommand = new LoginDataRequest(email, password);

            var account = await _authApiService.Login(loginCommand);

            await _tokenRepository.SetToken(account.Token);
        }
    }
}
