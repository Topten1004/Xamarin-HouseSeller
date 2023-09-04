using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Propertíes.Models.Create.Modals
{
    public class TooManyPropertiesViewModel : BaseModalViewModel
    {
        private ICommand _shareApp;

        public TooManyPropertiesViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService) : base(busyManager, navigationService)
        {
        }

        public ICommand ShareApp => _shareApp ??=
            new Command(async () => await Share.RequestAsync("https://play.google.com/"));
    }
}
