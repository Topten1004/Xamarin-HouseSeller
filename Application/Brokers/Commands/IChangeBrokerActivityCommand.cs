using System.Threading.Tasks;

namespace Immowert4You.Application.Brokers.Commands
{
    public interface IChangeBrokerActivityCommand
    {
        Task Execute(bool value);
    }
}
