using Immowert4You.Domain.Brokers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Immowert4You.Application.Brokers.GetBrokers
{
    public interface IGetBrokersQuery
    {
        Task<List<BrokerDto>> Execute();
    }
}
