using Newtonsoft.Json;

namespace Immowert4You.Domain.Account
{
    public class AccountDto
    {
        [JsonRequired]
        public string Token { get; set; }
    }
}
