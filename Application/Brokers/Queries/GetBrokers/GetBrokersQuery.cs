using Immowert4You.Application.Contracts.API;
using Immowert4You.Domain.Brokers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Brokers.GetBrokers
{
    public class GetBrokersQuery : IGetBrokersQuery
    {
        private readonly IBrokersApiService _brokersApiService;

        public GetBrokersQuery(IBrokersApiService brokersApiService)
        {
            _brokersApiService = brokersApiService;
        }

        public Task<List<BrokerDto>> Execute()
        {
            return _brokersApiService.GetBrokers();
        }
    }
}
