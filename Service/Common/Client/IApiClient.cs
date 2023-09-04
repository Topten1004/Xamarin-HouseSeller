using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Immowert4You.Service.Common.Client
{
    public interface IApiClient
    {
        Task<T> SendRequestWithResponseAsync<T>(string endpoint, object data = default, Method method = Method.GET, CancellationToken cancelationToken = default);

        Task SendRequestAsync(string endpoint, object data = default, Method method = Method.POST, CancellationToken cancelationToken = default);

        Task SendFile(string endpoint, byte[] bytes, string fileName);
    }
}
