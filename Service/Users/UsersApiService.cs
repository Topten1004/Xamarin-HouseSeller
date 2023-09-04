using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Users.Commands.AddPhoneNumber;
using Immowert4You.Application.Users.Commands.ConfirmPhoneNumber;
using Immowert4You.Domain.Users;
using Immowert4You.Service.Common.Client;
using RestSharp;
using System.Threading.Tasks;

namespace Immowert4You.Service.Users
{
    public class UsersApiService : IUsersApiService
    {
        private readonly IApiClient _apiClient;

        public UsersApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task AddPhoneNumber(string userId, PhoneNumberDataRequest phoneData)
        {
            return _apiClient.SendRequestWithResponseAsync<UserDto>($"/api/Users/{userId}/phone-number/add", phoneData, Method.POST);
        }

        public Task ConfirmPhoneNumber(string userId, ConfirmCodeDataRequest codeData)
        {
            return _apiClient.SendRequestWithResponseAsync<UserDto>($"/api/Users/{userId}/phone-number/confirmation", codeData, Method.POST);
        }

        public Task<UserDto> GetCurrentUser()
        {
            return _apiClient.SendRequestWithResponseAsync<UserDto>("/api/Users/current");
        }

        public Task UpdateUser(UserDto user)
        {
            return _apiClient.SendRequestAsync($"/api/Users/{user.Id}", user, Method.PUT);
        }
    }
}
