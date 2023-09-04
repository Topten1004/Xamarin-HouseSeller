using System.Collections.Generic;

namespace Immowert4You.Domain.Chats
{
    public class ChatDto
    {
        public string Id { get; set; }
        public virtual string Customer { get; set; }
        public virtual string Broker { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual List<MessageDto> Messages { get; set; }
    }
}
