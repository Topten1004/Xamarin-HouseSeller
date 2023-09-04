using Immowert4You.Application.Account.Commands.ConfirmPhone;
using Immowert4You.Application.Account.Commands.Login;
using Immowert4You.Application.Account.Commands.Register;
using Immowert4You.Application.Account.Commands.RestorePassword;
using Immowert4You.Domain.Account;
using System;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IAccountApiService
    {
        event EventHandler LogoutEvent;

        void NotifyLogout();

        Task<AccountDto> Login(LoginDataRequest loginCommand);

        Task<AccountDto> RegisterPhone(RegisterPhoneRequest registerCommand);

        Task ConfirmPhone(ConfirmPhoneRequest confirmPhoneRequest);

        Task RestorePassword(RestorePasswordCommand restorePasswordCommand);
    }
}
