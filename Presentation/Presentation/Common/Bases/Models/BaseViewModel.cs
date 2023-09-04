using System.Windows.Input;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.NotifyChange;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Bases.Models
{
    public abstract class BaseViewModel : PropertyChangeHelper
    {
        protected readonly IBusyManager _busyManager;
        protected readonly INavigationService _navigationService;
        private ICommand _popAsync;
        private ICommand _popToRootAsync;
        private string _header;

        public BaseViewModel(IBusyManager busyManager, INavigationService navigationService)
        {
            _busyManager = busyManager;
            _navigationService = navigationService;

            _busyManager.BusyChangedEvent += BusyManager_BusyChangedEvent;
        }

        public string Header
        {
            get => _header;
            set => RiseAndSetIfChanged(ref _header, value);
        }

        public bool Busy
        {
            get => _busyManager.IsBusy;
        }

        public ICommand PopAsync => _popAsync ??= new Command(async () => await _navigationService.PopAsync());

        public ICommand PopToRootAsync => _popToRootAsync ??= new Command(async () => await _navigationService.PopToRootAsync());


        private void BusyManager_BusyChangedEvent(object sender, BusyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OnPropertyChanged("Busy");
            });
        }
    }
}
