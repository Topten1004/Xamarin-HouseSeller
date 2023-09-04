using System;
using System.Collections.Generic;
using System.Text;

namespace Immowert4You.Application.Account.Commands.ConfirmPhone
{
    public class ConfirmPhoneRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
