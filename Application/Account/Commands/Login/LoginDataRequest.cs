using Newtonsoft.Json;

namespace Immowert4You.Application.Account.Commands.Login
{
    public class LoginDataRequest
    {
        public LoginDataRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("password")]
        public string Password { get; }
    }
}
