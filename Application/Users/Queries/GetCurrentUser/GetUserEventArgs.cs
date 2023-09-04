using Immowert4You.Domain.Users;
using System;

namespace Immowert4You.Application.Users.Queries
{
    public class GetUserEventArgs : EventArgs
    {
        public UserDto User;
    }
}