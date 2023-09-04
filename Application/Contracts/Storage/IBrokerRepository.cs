using Immowert4You.Domain.Brokers;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface IBrokerRepository
    {
        Task SetBroker(BrokerDto brokerDto);
        BrokerDto GetBroker();
    }
}
