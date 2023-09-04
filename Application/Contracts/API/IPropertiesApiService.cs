using Immowert4You.Application.Properties.Commands.AddPropertyPrice;
using Immowert4You.Domain.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IPropertiesApiService
    {
        Task SendProperty(PropertyDto propertyDto);
        Task UpdateProperty(PropertyDto propertyDto);
        Task UploadPhoto(string propertyId, byte[] bytes, string fileName);
        Task AcceptProperty(string propertyId);
        Task DenyProperty(string propertyId);
        Task AddPropertyPrice(string propertyId, PropertyPriceDataRequest propertyPriceData);
        Task<List<PropertyDto>> GetBrokerProperties();
    }
}
