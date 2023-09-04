using Immowert4You.Application.Account.Commands.ConfirmPhone;
using Immowert4You.Application.Account.Commands.Login;
using Immowert4You.Application.Account.Commands.Register;
using Immowert4You.Application.Account.Commands.RestorePassword;
using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Account;
using Immowert4You.Service.Common.Client;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Immowert4You.Service.Authentications
{
    public class AccountApiService : IAccountApiService
    {
        private readonly IApiClient _apiClient;
        private readonly ITokenRepository _tokenRepository;

        public event EventHandler LogoutEvent;

        public AccountApiService(ITokenRepository tokenRepository, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _tokenRepository = tokenRepository;
        }

        public Task<AccountDto> Login(LoginDataRequest loginData)
        {
            _tokenRepository.SetToken(string.Empty);

            return _apiClient.SendRequestWithResponseAsync<AccountDto>("/api/account/auth", loginData, Method.POST);
        }

        public Task<AccountDto> RegisterPhone(RegisterPhoneRequest registerData)
        {
            return _apiClient.SendRequestWithResponseAsync<AccountDto>("/api/account/auth/phone", registerData, Method.POST);
        }

        public Task ConfirmPhone(ConfirmPhoneRequest confirmPhoneRequest)
        {
            return _apiClient.SendRequestAsync("/api/account/auth/phone/confirm", confirmPhoneRequest);
        }

        public async Task RestorePassword(RestorePasswordCommand restorePasswordCommand)
        {
            await _apiClient.SendRequestAsync("/api/account/forgottenPassword", restorePasswordCommand);
        }

        public void NotifyLogout()
        {
            _tokenRepository.SetToken(string.Empty);
            LogoutEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
