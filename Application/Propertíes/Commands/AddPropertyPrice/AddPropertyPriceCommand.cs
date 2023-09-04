using Immowert4You.Application.Contracts.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.AddPropertyPrice
{
    public class AddPropertyPriceCommand : IAddPropertyPriceCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public AddPropertyPriceCommand(IPropertiesApiService propertiesApiService)
        {
            _propertiesApiService = propertiesApiService;
        }
        public Task Execute(string propertyId, int price)
        {
            var priceData = new PropertyPriceDataRequest(price);

            return _propertiesApiService.AddPropertyPrice(propertyId, priceData);
        }
    }
}
