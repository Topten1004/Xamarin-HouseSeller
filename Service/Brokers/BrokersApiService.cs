using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Brokers;
using Immowert4You.Service.Common.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Service.Brokers
{
    public class BrokersApiService : IBrokersApiService
    {
        private readonly IApiClient _apiClient;

        public BrokersApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task ChangeBrokerActivity(string brokerId, bool value)
        {
            var variant = value ? "activate" : "deactivate";

            return _apiClient.SendRequestAsync($"/api/Brokers/{brokerId}/{variant}");
        }

        public Task<BrokerDto> GetBroker(string brokerId)
        {
            return _apiClient.SendRequestWithResponseAsync<BrokerDto>($"/api/Brokers/{brokerId}");
        }

        public Task<List<BrokerDto>> GetBrokers()
        {
            return _apiClient.SendRequestWithResponseAsync<List<BrokerDto>>("/api/Brokers");
        }
    }
}
