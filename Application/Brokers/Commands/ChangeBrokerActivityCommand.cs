using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System.Threading.Tasks;

namespace Immowert4You.Application.Brokers.Commands
{
    public class ChangeBrokerActivityCommand : IChangeBrokerActivityCommand
    {
        private readonly IBrokersApiService _brokersApiService;
        private readonly IBrokerRepository _brokerRepository;

        public ChangeBrokerActivityCommand(
            IBrokersApiService brokersApiService,
            IBrokerRepository brokerRepository)
        {
            _brokersApiService = brokersApiService;
            _brokerRepository = brokerRepository;
        }

        public Task Execute(bool value)
        {
            var brokerId = _brokerRepository.GetBroker().Id;

            return _brokersApiService.ChangeBrokerActivity(brokerId, value);
        }
    }
}
