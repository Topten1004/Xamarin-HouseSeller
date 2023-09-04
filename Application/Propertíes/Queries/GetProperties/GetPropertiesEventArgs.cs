using Immowert4You.Domain.Properties;
using System;
using System.Collections.Generic;

namespace Immowert4You.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesEventArgs : EventArgs
    {
        public List<PropertyDto> Properties;
    }
}
