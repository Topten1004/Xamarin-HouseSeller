using System.Threading.Tasks;

namespace Immowert4You.Application.Account.Commands.Register
{
    public interface IRegisterPhoneCommand
    {
        Task Execute(string gender, string firstName, string lastName, string phoneNumber);
    }
}
