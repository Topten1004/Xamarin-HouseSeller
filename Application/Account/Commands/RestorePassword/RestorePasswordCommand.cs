using Newtonsoft.Json;

namespace Immowert4You.Application.Account.Commands.RestorePassword
{
    public class RestorePasswordCommand
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
