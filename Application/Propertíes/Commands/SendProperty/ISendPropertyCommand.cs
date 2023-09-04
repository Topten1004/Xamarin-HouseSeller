using Immowert4You.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.SendProperty
{
    public interface ISendPropertyCommand
    {
        Task Execute(PropertyDto propertyDto);
    }
}
