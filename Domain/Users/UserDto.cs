using Immowert4You.Domain.Properties;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Immowert4You.Domain.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsBroker { get; set; }
        public string PlayerId { get; set; }
        public string PhoneToken { get; set; }
    }
}
