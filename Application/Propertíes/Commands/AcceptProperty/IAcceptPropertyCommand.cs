using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.AcceptProperty
{
    public interface IAcceptPropertyCommand
    {
        Task Execute(string propertyId);
    }
}
