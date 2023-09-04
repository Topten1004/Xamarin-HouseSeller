using Immowert4You.Domain.Properties;
using System.Threading.Tasks;

namespace Immowert4You.Application.Propertíes.Commands.UpdateProperty
{
    public interface IUpdatePropertyCommand
    {
        Task Execute(PropertyDto propertyDto);
    }
}
