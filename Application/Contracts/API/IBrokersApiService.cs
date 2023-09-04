using Immowert4You.Domain.Brokers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.API
{
    public interface IBrokersApiService
    {
        Task ChangeBrokerActivity(string brokerId, bool value);

        Task<BrokerDto> GetBroker(string brokerId);

        Task<List<BrokerDto>> GetBrokers();
    }
}
