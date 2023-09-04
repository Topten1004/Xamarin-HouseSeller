using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Presentation.Brokers.Views;
using Immowert4You.Presentation.Customers.Views;
using Immowert4You.Presentation.Home.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Home.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BurgerPage : FlyoutPage
    {
        public BurgerPage(
            BurgerViewModel viewModel,
            ICurrentUserRepository currentUserRepository)
        {
            BindingContext = viewModel;

            var user = currentUserRepository.GetUser();

            if (user is null || !currentUserRepository.GetUser().IsBroker)
            {
                Detail = new NavigationPage(
                    Application.TinyIoC.TinyIoCContainer
                    .Current.Resolve<UserTabbedPage>());
            }
            else
            {
                Detail = new NavigationPage(
                    Application.TinyIoC.TinyIoCContainer
                    .Current.Resolve<BrokerTabbedPage>());
            }

            InitializeComponent();
        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list.SelectedItem = null;

            IsPresented = false;

            if (e.SelectedItem != null)
            {
                await Task.Delay(300);

                (e.SelectedItem as BurgerMenuItem)
                    .Command
                    .Execute(null);
            }
        }
    }
}