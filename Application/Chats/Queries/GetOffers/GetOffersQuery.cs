using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Chats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Queries.GetOffers
{
    public class GetOffersQuery : IGetOffersQuery
    {
        private readonly IChatsApiService _chatsApiService;

        public GetOffersQuery(IChatsApiService chatsApiService)
        {
            _chatsApiService = chatsApiService;
        }

        public async Task<List<OfferDto>> Execute()
        {
            var offers = await _chatsApiService.GetOffers();

            return offers ??= new List<OfferDto>();
        }
    }
}
