using Immowert4You.Application.Chats.Commands.SendMessage;
using Immowert4You.Domain.Chats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IChatsApiService
    {
        Task<List<ChatDto>> GetChats();
        Task SendMessage(string chatId, SendMessageDataRequest sendMessageData);
        Task<List<OfferDto>> GetOffers();
    }
}
