using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Properties.Commands.AddPropertyPrice;
using Immowert4You.Domain.Properties;
using Immowert4You.Service.Common.Client;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Service.Properties_
{
    public class PropertiesApiService : IPropertiesApiService
    {
        private readonly IApiClient _apiClient;

        public PropertiesApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task SendProperty(PropertyDto propertyDto)
        {
            return _apiClient.SendRequestAsync("/api/Properties", propertyDto);
        }

        public Task UpdateProperty(PropertyDto propertyDto)
        {
            return _apiClient.SendRequestAsync($"/api/Properties/{propertyDto.Id}", propertyDto, Method.PUT);
        }

        public Task UploadPhoto(string propertyId, byte[] bytes, string fileName)
        {
            return _apiClient.SendFile($"api/Properties/{propertyId}/upload", bytes, fileName);
        }

        public Task AcceptProperty(string propertyId)
        {
            return _apiClient.SendRequestAsync($"/api/Properties/{propertyId}/agreed");
        }

        public Task DenyProperty(string propertyId)
        {
            return _apiClient.SendRequestAsync($"/api/Properties/{propertyId}/deny");
        }

        public Task AddPropertyPrice(string propertyId, PropertyPriceDataRequest propertyPriceData)
        {
            return _apiClient.SendRequestAsync($"/api/Properties/{propertyId}/estimation", propertyPriceData);
        }

        public Task<List<PropertyDto>> GetBrokerProperties()
        {
            return _apiClient.SendRequestWithResponseAsync<List<PropertyDto>>("/api/Brokers/current/properties");
        }
    }
}
