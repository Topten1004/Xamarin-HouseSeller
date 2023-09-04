using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.ConfirmPhoneNumber
{
    public class ConfirmPhoneNumberCommand : IConfirmPhoneNumberCommand
    {
        private readonly IUsersApiService _usersApiService;
        private readonly ICurrentUserRepository _currentUserRepository;

        public ConfirmPhoneNumberCommand(IUsersApiService usersApiService, ICurrentUserRepository currentUserRepository)
        {
            _usersApiService = usersApiService;
            _currentUserRepository = currentUserRepository;
        }

        public Task Execute(string code)
        {
            var user = _currentUserRepository.GetUser();

            var codeDataRequest = new ConfirmCodeDataRequest { Code = code };

            return _usersApiService.ConfirmPhoneNumber(user.Id, codeDataRequest);
        }
    }
}
