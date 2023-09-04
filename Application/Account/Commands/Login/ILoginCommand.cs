using Immowert4You.Domain.Account;
using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.Login
{
    public interface ILoginCommand
    {
        Task Execute(string email, string password);
    }
}
