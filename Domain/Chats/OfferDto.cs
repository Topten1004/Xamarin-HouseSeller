using Immowert4You.Domain.Properties;

namespace Immowert4You.Domain.Chats
{
    public class OfferDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string ZipCode { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
