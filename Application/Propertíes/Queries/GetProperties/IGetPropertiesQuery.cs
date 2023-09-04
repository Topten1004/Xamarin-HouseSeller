using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Immowert4You.Domain.Properties;

namespace Immowert4You.Application.Properties.Queries.GetProperties
{
    public interface IGetPropertiesQuery
    {
        Task Execute();

        event EventHandler<List<PropertyDto>> UserPropertiesExecuted;
        event EventHandler<List<PropertyDto>> BrokerPropertiesExecuted;
    }
}
