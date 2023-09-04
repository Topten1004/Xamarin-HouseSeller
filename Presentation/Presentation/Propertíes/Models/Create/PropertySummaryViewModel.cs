using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Commands.SendProperty;
using Immowert4You.Application.Propertíes.Commands.UpdateProperty;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Propertíes.Views.Create.Modals;
using Immowert4You.Presentation.Properties.Views.Extras;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertySummaryViewModel : BaseViewModel
    {
        private readonly IUpdatePropertyCommand _updatePropertyCommand;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IGetPropertiesQuery _getPropertiesQuery;

        private ICommand _navigateToHouseExtraRooms;
        private ICommand _navigateToSendPremiumPropertyModalPage;
        private ICommand _navigateToSaveAndQuitModalPage;


        public PropertySummaryViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ITempStorage tempStorage,
            IUpdatePropertyCommand updatePropertyCommand,
            IGetCurrentUserQuery getCurrentUserQuery,
            IPropertyRepository propertyRepository,
            IGetPropertiesQuery getPropertiesQuery) : base(busyManager, navigationService)
        {
            _updatePropertyCommand = updatePropertyCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _propertyRepository = propertyRepository;
            _getPropertiesQuery = getPropertiesQuery;

            Header = "Teil 5/5";

            Property = tempStorage.Read<PropertyDto>();

            IsExtraButtonVisible = Property.IntentionToSell >= 8;
        }

        public PropertyDto Property { get; }

        public bool IsParcel => Property.Type == PropertyType.Parcel;

        public bool IsExtraButtonVisible { get; }

        public ICommand NavigateToHouseExtraRooms => _navigateToHouseExtraRooms ??= new Command(
            async () => await _navigationService.PushAsync<PropertyExtraRoomsPage>());
        public ICommand NavigateToSendPremiumPropertyModalPage => _navigateToSendPremiumPropertyModalPage ??= new Command(
            async () => {

                await _busyManager.SetBusy();

                try
                {
                    await _getPropertiesQuery.Execute();

                    var property = _propertyRepository.GetProperties()?.FirstOrDefault();

                    Property.Id = property?.Id;

                    Property.IsUrgent = true;

                    await _updatePropertyCommand.Execute(Property);

                    await _getCurrentUserQuery.Execute();

                    await _navigationService.PopToRootAsync();
                }
                catch (Exception ex)
                {
                    await PopUpHelper.ShowAlert("Error", ex.Message);
                }
                finally
                {
                    await _busyManager.SetUnBusy();
                }

            });
        public ICommand NavigateToSaveAndQuitModalPage => _navigateToSaveAndQuitModalPage ??= new Command(
            async () => await _navigationService.PopToRootAsync());

 
    }
}
