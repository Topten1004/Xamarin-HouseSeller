using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Properties.Views.Extras;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Create.Extras
{
    public class PropertyExtraRoomsViewModel : BaseViewModel
    {
        private ICommand _navigateToHouseCosts;

        public PropertyExtraRoomsViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage) : base(busyManager, navigationService)
        {
            Header = "Zusatz 1/2";

            Property = tempStorage.Read<PropertyDto>();
        }

        public bool IsFlat => Property.Type == PropertyType.Apartment;

        public PropertyDto Property { get; }

        public ICommand NavigateToHouseCosts => _navigateToHouseCosts ??= new Command(
            async () => await _navigationService.PushAsync<PropertyExtraInfoPage, PropertyDto>(Property));
    }
}
