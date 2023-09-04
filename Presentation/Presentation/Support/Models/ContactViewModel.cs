using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Support.Models
{
    public class ContactViewModel : BaseViewModel
    {
        private ICommand _sendMessage;
        private string _emailAddress;
        private string _name;
        private string _surname;
        private string _message;
        private INavigationService _navigationService; 
        public ContactViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService) : base(busyManager, navigationService)
        {
            Header = "Kontakt";
            _navigationService = navigationService;
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set => RiseAndSetIfChanged(ref _emailAddress, value);
        }

        public string Name
        {
            get => _name;
            set => RiseAndSetIfChanged(ref _name, value);
        }

        public string Surname
        {
            get => _surname;
            set => RiseAndSetIfChanged(ref _surname, value);
        }

        public string Message
        {
            get => _message;
            set => RiseAndSetIfChanged(ref _message, value);
        }

        public ICommand SendMessage => _sendMessage ??=
            new Command(async () => await Send());

        public async Task Send()
        {
            await Email.ComposeAsync(new EmailMessage
            {
                To = { "office@immowert4you.app" },
                Body = _message,
                Subject = $"Nachricht von {_name} {_surname} ({_emailAddress})"
            });
            await _navigationService.PopAsync();
        }
    }
}
