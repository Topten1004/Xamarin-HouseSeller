using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Propertíes.Commands.DenyProperty
{
    public interface IDenyPropertyCommand
    {
        Task Execute(string propertyId);
    }
}
