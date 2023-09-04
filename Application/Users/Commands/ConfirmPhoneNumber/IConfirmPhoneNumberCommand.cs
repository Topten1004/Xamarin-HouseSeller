using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.ConfirmPhoneNumber
{
    public interface IConfirmPhoneNumberCommand
    {
        Task Execute(string code);
    }
}
