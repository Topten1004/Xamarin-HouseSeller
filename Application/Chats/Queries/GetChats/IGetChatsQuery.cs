using Immowert4You.Domain.Chats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Chats.Queries.GetChats
{
    public interface IGetChatsQuery
    {
        Task<List<ChatDto>> Execute();
    }
}
