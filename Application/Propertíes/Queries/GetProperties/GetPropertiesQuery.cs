using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesQuery : IGetPropertiesQuery
    {
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly ICustomersApiService _customersApiService;
        private readonly IPropertiesApiService _propertiesApiService;
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertiesQuery(
            ICurrentUserRepository currentUserRepository,
            ICustomersApiService customersApiService,
            IPropertiesApiService propertiesApiService,
            IPropertyRepository propertyRepository)
        {
            _currentUserRepository = currentUserRepository;
            _customersApiService = customersApiService;
            _propertiesApiService = propertiesApiService;
            _propertyRepository = propertyRepository;
        }

        public event EventHandler<List<PropertyDto>> UserPropertiesExecuted;
        public event EventHandler<List<PropertyDto>> BrokerPropertiesExecuted;

        public async Task Execute()
        {
            var user = _currentUserRepository.GetUser();

            var properties = new List<PropertyDto>();

            try
            {
                if (user.IsBroker)
                {
                    var brokerProperties = await _propertiesApiService.GetBrokerProperties();

                    if (brokerProperties != null)
                    {
                        properties = brokerProperties;

                        BrokerPropertiesExecuted?.Invoke(this, properties);
                    }
                        
                }
                else
                {
                    var property = await _customersApiService.GetUserProperty(user.Id);

                    if (property != null)
                    {
                        properties.Add(property);

                        UserPropertiesExecuted?.Invoke(this, properties);
                    }
                }

                await _propertyRepository.SetProperties(properties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
