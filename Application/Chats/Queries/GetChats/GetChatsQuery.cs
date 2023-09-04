using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Chats;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Queries.GetChats
{
    public class GetChatsQuery : IGetChatsQuery
    {
        private readonly IChatsApiService _chatsApiService;

        public GetChatsQuery(IChatsApiService chatsApiService)
        {
            _chatsApiService = chatsApiService;
        }

        public async Task<List<ChatDto>> Execute()
        {
            try
            {
                var chats = await _chatsApiService.GetChats();

                return chats ??= new List<ChatDto>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
