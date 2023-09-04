using Immowert4You.Domain.Chats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface IChatRepository
    {
        Task SetChats(List<ChatDto> chats);
        List<ChatDto> GetChats();
    }
}
