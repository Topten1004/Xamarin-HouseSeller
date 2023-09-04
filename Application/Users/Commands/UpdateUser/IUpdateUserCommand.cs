using Immowert4You.Domain.Users;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.UpdateUser
{
    public interface IUpdateUserCommand
    {
        Task Execute(UserDto user);
    }
}
