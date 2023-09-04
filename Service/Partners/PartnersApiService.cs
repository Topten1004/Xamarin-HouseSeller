using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Partners;
using Immowert4You.Service.Common.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Service.Partners
{
    public class PartnersApiService : IPartnersApiService
    {
        private readonly IApiClient _apiClient;

        public PartnersApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public Task<IEnumerable<PartnerDto>> GetPartners()
        {
            return _apiClient.SendRequestWithResponseAsync<IEnumerable<PartnerDto>>("api/Partners");
        }
    }
}
