using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Chats.Views;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Customers.Views;
using Immowert4You.Presentation.Home.Views;
using Immowert4You.Presentation.Properties.Views.Create;
using Immowert4You.Presentation.Propertíes.Views.Create.Modals;
using Immowert4You.Presentation.Properties.Views.Expose;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertyTypeViewModel : BaseViewModel
    {
        private ICommand _navigateToSubcategoryPage;
        private ICommand _navigateToValue;
        private ICommand _navigateToReviewsPage;
        private ICommand _navigateToMessagesListPage;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IPropertyRepository _propertyRepository;

        public PropertyTypeViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ICurrentUserRepository currentUserRepository,
            IPropertyRepository propertyRepository) : base(busyManager, navigationService)
        {
            Header = "Kostenlose Bewertung";

            _currentUserRepository = currentUserRepository;
            _propertyRepository = propertyRepository;
        }

        internal void ResetHome()
        {
            _navigationService.SetMainPage<BurgerPage>();
        }

        public ICommand NavigateToSubcategoryPage => _navigateToSubcategoryPage ??=
            new Command<int>(async (subjectId) => await NavigateToSubcategoryPageExecute(subjectId));

        public ICommand NavigateToMessagesListPage => _navigateToMessagesListPage ??=
            new Command(async () => await _navigationService.PushAsync<ChatPage>());

        public ICommand NavigateToValue => _navigateToValue ??=
            new Command(async () => await _navigationService.PushAsync<IncreasePropertyValuePage>());

        public ICommand NavigateToReviewsPage => _navigateToReviewsPage ??=
            new Command(async () => await _navigationService.PushAsync<ExposedPropertyPage>());

        private async Task NavigateToSubcategoryPageExecute(int subjectId)
        {
            var user = _currentUserRepository.GetUser();

            if (user is null || _propertyRepository.GetProperties()?.Any() != true)
            {
                var newProperty = new PropertyDto { Type = (PropertyType)subjectId };

                await _navigationService.PushAsync<PropertySubcategoryPage, PropertyDto>(newProperty);

                return;
            }

            await _navigationService.PushModalAsync<TooManyPropertiesModal>();
        }
    }
}
