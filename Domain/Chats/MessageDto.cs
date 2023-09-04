using Immowert4You.Domain.Users;

namespace Immowert4You.Domain.Chats
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public bool IsOwnMessage { get; set; }
    }
}
