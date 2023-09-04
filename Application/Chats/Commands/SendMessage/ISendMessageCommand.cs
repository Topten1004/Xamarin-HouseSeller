using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Commands.SendMessage
{
    public interface ISendMessageCommand
    {
        Task Execute(string chatId, string text);
    }
}
