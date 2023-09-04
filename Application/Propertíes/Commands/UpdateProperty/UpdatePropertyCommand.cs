using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Propertíes.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IUpdatePropertyCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public UpdatePropertyCommand(IPropertiesApiService propertiesApiService)
        {
            _propertiesApiService = propertiesApiService;
        }

        public Task Execute(PropertyDto propertyDto)
        {
            return _propertiesApiService.UpdateProperty(propertyDto);
        }
    }
}
