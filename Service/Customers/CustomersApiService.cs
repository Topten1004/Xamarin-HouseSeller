using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Properties;
using Immowert4You.Service.Common.Client;
using System.Threading.Tasks;

namespace Immowert4You.Service.Customers
{
    public class CustomersApiService : ICustomersApiService
    {
        private readonly IApiClient _apiClient;

        public CustomersApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<PropertyDto> GetUserProperty(string userId)
        {
            return _apiClient.SendRequestWithResponseAsync<PropertyDto>($"/api/Customers/{userId}/properties");
        }
    }
}
