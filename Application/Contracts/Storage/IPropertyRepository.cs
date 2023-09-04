using Immowert4You.Domain.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface IPropertyRepository
    {
        Task SetProperties(List<PropertyDto> properties);
        List<PropertyDto> GetProperties();
    }
}
