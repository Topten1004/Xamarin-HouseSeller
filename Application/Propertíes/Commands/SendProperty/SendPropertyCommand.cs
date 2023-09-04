using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Properties;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.SendProperty
{
    public class SendPropertyCommand : ISendPropertyCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public SendPropertyCommand(IPropertiesApiService propertiesApiService)
        {
            _propertiesApiService = propertiesApiService;
        }

        public Task Execute(PropertyDto propertyDto)
        {
            return _propertiesApiService.SendProperty(propertyDto);
        }
    }
}
