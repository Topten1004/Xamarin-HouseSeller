using Immowert4You.Application.Contracts.Storage;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ITempStorage _tempStorage;

        public NavigationService(ITempStorage tempStorage)
        {
            _tempStorage = tempStorage;
        }
        public Task PopAsync()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

        public Task PopModalAsync()
        {
            return App.Current.MainPage.Navigation.PopModalAsync();
        }

        public Task PopToRootAsync()
        {
            return App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public Task PushAsync<TPage, TEntity>(TEntity toSave)
            where TPage : Page
            where TEntity : class
        {
            _tempStorage.Save(toSave);
            return PushAsync<TPage>();
        }

        public Task PushAsync<TPage>() where TPage : Page
        {
            return App.Current.MainPage.Navigation.PushAsync(
                Application.TinyIoC.TinyIoCContainer.Current.Resolve<TPage>());
        }

        public Task PushModalAsync<TPage, TEntity>(TEntity toSave)
            where TPage : Page
            where TEntity : class
        {
            _tempStorage.Save(toSave);
            return PushModalAsync<TPage>();
        }

        public Task PushModalAsync<TPage>() where TPage : Page
        {
            return App.Current.MainPage.Navigation.PushModalAsync(
                Application.TinyIoC.TinyIoCContainer.Current.Resolve<TPage>());
        }

        public void SetMainPage<TPage>() where TPage : Page
        {
            App.Current.MainPage = new NavigationPage(Application.TinyIoC
                .TinyIoCContainer.Current.Resolve<TPage>());
        }
    }
}
