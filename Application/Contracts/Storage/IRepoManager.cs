using System.Threading.Tasks;

namespace Immowert4You.Application.Contracts.Storage
{
    public interface IRepoManager
    {
        Task FetchRepositories();
    }
}
