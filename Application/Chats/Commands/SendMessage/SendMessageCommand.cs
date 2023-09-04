using Immowert4You.Application.Contracts.API;
using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Commands.SendMessage
{
    public class SendMessageCommand : ISendMessageCommand
    {
        private readonly IChatsApiService _chatsApiService;

        public SendMessageCommand(IChatsApiService chatsApiService)
        {
            _chatsApiService = chatsApiService;
        }

        public Task Execute(string chatId, string text)
        {
            var sendMessageData = new SendMessageDataRequest(text);

            return _chatsApiService.SendMessage(chatId, sendMessageData);
        }
    }
}
