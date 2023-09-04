using Immowert4You.Domain.Users;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface ICurrentUserRepository
    {
        Task SetUser(UserDto currentUser);
        UserDto GetUser();
    }
}
