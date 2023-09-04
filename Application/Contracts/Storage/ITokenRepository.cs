using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface ITokenRepository
    {
        Task SetToken(string token);
        string GetToken();
    }
}
