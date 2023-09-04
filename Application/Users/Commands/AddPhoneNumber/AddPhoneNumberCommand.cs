using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.AddPhoneNumber
{
    public class AddPhoneNumberCommand : IAddPhoneNumberCommand
    {
        private readonly IUsersApiService _usersApiService;
        private readonly ICurrentUserRepository _currentUserRepository;

        public AddPhoneNumberCommand(IUsersApiService usersApiService, ICurrentUserRepository currentUserRepository)
        {
            _usersApiService = usersApiService;
            _currentUserRepository = currentUserRepository;
        }

        public Task Execute(string gender, string firstName, string lastName, string phoneNumber)
        {
            var user = _currentUserRepository.GetUser();

            var phoneNumberData = new PhoneNumberDataRequest { PhoneNumber = phoneNumber };

            var response = _usersApiService.AddPhoneNumber(user.Id, phoneNumberData);

            return response;
        }
    }
}
