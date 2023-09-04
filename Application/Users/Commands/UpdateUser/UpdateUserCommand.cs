using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IUsersApiService _usersApiService;

        public UpdateUserCommand(IUsersApiService usersApiService)
        {
            _usersApiService = usersApiService;
        }

        public Task Execute(UserDto user)
        {
            return _usersApiService.UpdateUser(user);
        }
    }
}
