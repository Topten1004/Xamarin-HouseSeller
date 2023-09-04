using Immowert4You.Application.Chats.Commands.SendMessage;
using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Chats;
using Immowert4You.Service.Common.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Service.Chats
{
    public class ChatsApiService : IChatsApiService
    {
        private readonly IApiClient _apiClient;

        public ChatsApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task<List<ChatDto>> GetChats()
        {
            return _apiClient.SendRequestWithResponseAsync<List<ChatDto>>("/api/Users/current/chats");
        }

        public Task<List<OfferDto>> GetOffers()
        {
            return _apiClient.SendRequestWithResponseAsync<List<OfferDto>>("/api/Users/current/offerts");
        }

        public Task SendMessage(string chatId, SendMessageDataRequest sendMessageData)
        {
            return _apiClient.SendRequestAsync($"/api/Chats/{chatId}/messages", sendMessageData);
        }
    }
}
