using Immowert4You.Domain.Partners;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Partners.Queries.GetPartners
{
    public interface IGetPartnersQuery
    {
        Task<IEnumerable<PartnerDto>> Execute();
    }
}
