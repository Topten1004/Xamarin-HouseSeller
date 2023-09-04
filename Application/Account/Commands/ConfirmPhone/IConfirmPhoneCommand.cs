using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.ConfirmPhone
{
    public interface IConfirmPhoneCommand
    {
        Task Execute(string token);
    }
}
