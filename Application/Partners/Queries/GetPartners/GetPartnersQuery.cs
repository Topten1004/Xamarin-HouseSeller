using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Partners;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Partners.Queries.GetPartners
{
    public class GetPartnersQuery : IGetPartnersQuery
    {
        private readonly IPartnersApiService _partnersApiService;

        public GetPartnersQuery(IPartnersApiService partnersApiService)
        {
            _partnersApiService = partnersApiService;
        }
        public Task<IEnumerable<PartnerDto>> Execute()
        {
            return _partnersApiService.GetPartners();
        }
    }
}
