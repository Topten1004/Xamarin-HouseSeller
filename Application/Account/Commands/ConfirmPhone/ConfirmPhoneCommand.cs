using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.ConfirmPhone
{
    public class ConfirmPhoneCommand : IConfirmPhoneCommand
    {
        private readonly IAccountApiService _accountApiService;
        private readonly ICurrentUserRepository _currentUserRepository;

        public ConfirmPhoneCommand(IAccountApiService accountApiService, ICurrentUserRepository currentUserRepository)
        {
            _accountApiService = accountApiService;
            _currentUserRepository = currentUserRepository;
        }
        public Task Execute(string token)
        {
            var user = _currentUserRepository.GetUser();

            var request = new ConfirmPhoneRequest
            {
                UserId = user.Id,
                Token = token,
            };

            return _accountApiService.ConfirmPhone(request);
        }
    }
}
