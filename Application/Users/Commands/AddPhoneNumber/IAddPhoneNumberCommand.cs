using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Commands.AddPhoneNumber
{
    public interface IAddPhoneNumberCommand
    {
        Task Execute(string gender, string firstName, string lastName, string phoneNumber);
    }
}
