using System.Threading.Tasks;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Services.Navigation
{
    public interface INavigationService
    {
        void SetMainPage<TPage>() where TPage : Page;
        Task PushAsync<TPage, TEntity>(TEntity toSave) where TPage : Page where TEntity : class;
        Task PushAsync<TPage>() where TPage : Page;
        Task PopAsync();
        Task PopToRootAsync();
        Task PushModalAsync<TPage, TEntity>(TEntity toSave) where TPage : Page where TEntity : class;
        Task PushModalAsync<TPage>() where TPage : Page;
        Task PopModalAsync();
    }
}
