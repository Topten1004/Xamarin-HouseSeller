using Immowert4You.Application.Contracts.API;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.AcceptProperty
{
    public class AcceptPropertyCommand : IAcceptPropertyCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public AcceptPropertyCommand(IPropertiesApiService propertiesApiService)
        {
            _propertiesApiService = propertiesApiService;
        }

        public Task Execute(string propertyId)
        {
            return _propertiesApiService.AcceptProperty(propertyId);
        }
    }
}
