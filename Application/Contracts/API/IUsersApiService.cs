using Immowert4You.Application.Users.Commands.AddPhoneNumber;
using Immowert4You.Application.Users.Commands.ConfirmPhoneNumber;
using Immowert4You.Domain.Users;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IUsersApiService
    {
        Task<UserDto> GetCurrentUser();
        Task UpdateUser(UserDto user);
        Task AddPhoneNumber(string userId, PhoneNumberDataRequest phoneNumberDataRequest);
        Task ConfirmPhoneNumber(string userId, ConfirmCodeDataRequest confirmCodeDataRequest);
    }
}
