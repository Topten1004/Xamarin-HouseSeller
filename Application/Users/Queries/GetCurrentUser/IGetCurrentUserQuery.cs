using Immowert4You.Domain.Users;
using System;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Queries
{
    public interface IGetCurrentUserQuery
    {
        Task<UserDto> Execute();

        public event EventHandler<GetUserEventArgs> GetUserExecuted;
    }
}
