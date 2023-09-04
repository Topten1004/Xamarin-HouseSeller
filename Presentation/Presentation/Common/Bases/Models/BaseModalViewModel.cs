using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Bases.Models
{
    public class BaseModalViewModel : BaseViewModel
    {
        private ICommand _closeModalCommand;

        public BaseModalViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService) : base(busyManager, navigationService)
        {
        }

        public Task OnModalAppearing()
        {
            return _busyManager.SetBusy();
        }

        public Task OnModalDisappearing()
        {
            return _busyManager.SetUnBusy();
        }

        public ICommand CloseModalCommand => _closeModalCommand ??= new Command(async () => await _navigationService.PopModalAsync());
    }
}
