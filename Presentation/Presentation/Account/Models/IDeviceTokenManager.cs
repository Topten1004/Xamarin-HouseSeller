using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Presentation.Account.Models
{
    public interface IDeviceTokenManager
    {
        string GetDeviceToken();
    }
}
