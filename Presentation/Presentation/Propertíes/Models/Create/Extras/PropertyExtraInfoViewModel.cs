using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Propertíes.Views.Create.Modals;
using System.Threading.Tasks;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Immowert4You.Application.Propertíes.Commands.UpdateProperty;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Application.Properties.Queries.GetProperties;
using System.Linq;

namespace Immowert4You.Presentation.Properties.Models.Create.Extras
{
    public class PropertyExtraInfoViewModel : BaseViewModel
    {
        private ICommand _navigateToSaveAndQuitModalPage;
        private IUpdatePropertyCommand _updatePropertyCommand;
        private IGetCurrentUserQuery _getCurrentUserQuery;
        private IPropertyRepository _propertyRepository;
        private IGetPropertiesQuery _getPropertiesQuery; 
        public PropertyExtraInfoViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage,
            IUpdatePropertyCommand updatePropertyCommand,
            IGetCurrentUserQuery getCurrentUserQuery,
            IPropertyRepository propertyRepository,
            IGetPropertiesQuery getPropertiesQuery) : base(busyManager, navigationService)
        {
            Header = "Zusatz 2/2";

            Property = tempStorage.Read<PropertyDto>();
            _updatePropertyCommand = updatePropertyCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _propertyRepository = propertyRepository;
            _getPropertiesQuery = getPropertiesQuery;
        }

        public PropertyDto Property { get; }
        public bool IsFlat => Property.Type == PropertyType.Apartment;
        public bool IsHouse => Property.Type == PropertyType.House;

        public ICommand NavigateToSaveAndQuitModalPage => _navigateToSaveAndQuitModalPage ??= new Command(
            async () => await SaveAndQuitExecute());

        private async Task SaveAndQuitExecute()
        {
            await _busyManager.SetBusy();

            try
            {
                await _getPropertiesQuery.Execute();

                var property = _propertyRepository.GetProperties()?.FirstOrDefault();

                Property.Id = property?.Id;

                await _updatePropertyCommand.Execute(Property);

                await _getCurrentUserQuery.Execute();

            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                await _busyManager.SetUnBusy();

                await _navigationService.PopToRootAsync();
            }
        }
    }
}
