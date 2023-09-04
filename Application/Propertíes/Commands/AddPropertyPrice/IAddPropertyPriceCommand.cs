using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.AddPropertyPrice
{
    public interface IAddPropertyPriceCommand
    {
        Task Execute(string propertyId, int price);
    }
}
