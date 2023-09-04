using Immowert4You.Application.Contracts.API;
using System.Threading.Tasks;

namespace Immowert4You.Application.Propertíes.Commands.DenyProperty
{
    public class DenyPropertyCommand : IDenyPropertyCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public DenyPropertyCommand(IPropertiesApiService propertiesApiService)
        {
            _propertiesApiService = propertiesApiService;
        }

        public Task Execute(string propertyId)
        {
            return _propertiesApiService.DenyProperty(propertyId);
        }
    }
}
