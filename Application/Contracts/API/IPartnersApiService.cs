using Immowert4You.Domain.Partners;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IPartnersApiService
    {
        Task<IEnumerable<PartnerDto>> GetPartners();
    }
}
