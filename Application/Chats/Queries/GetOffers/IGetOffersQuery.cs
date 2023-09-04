using Immowert4You.Domain.Chats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Queries.GetOffers
{
    public interface IGetOffersQuery
    {
        Task<List<OfferDto>> Execute();
    }
}
