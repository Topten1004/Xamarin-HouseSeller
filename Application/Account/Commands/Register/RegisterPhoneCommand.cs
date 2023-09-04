using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.Register
{
    public class RegisterPhoneCommand : IRegisterPhoneCommand
    {
        private readonly IAccountApiService _authApiService;
        private readonly ITokenRepository _tokenRepository;

        public RegisterPhoneCommand(
            IAccountApiService authApiService,
            ITokenRepository tokenRepository)
        {
            _authApiService = authApiService;
            _tokenRepository = tokenRepository;
        }
        public async Task Execute(string gender, string firstName, string lastName, string phoneNumber)
        {
            var registerDto = new RegisterPhoneRequest { Gender = gender, FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber };

            var account = await _authApiService.RegisterPhone(registerDto);

            await _tokenRepository.SetToken(account.Token);
        }
    }
}
