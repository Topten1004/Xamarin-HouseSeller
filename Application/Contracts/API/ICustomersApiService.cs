using Immowert4You.Domain.Properties;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface ICustomersApiService
    {
        Task<PropertyDto> GetUserProperty(string userId);
    }
}
