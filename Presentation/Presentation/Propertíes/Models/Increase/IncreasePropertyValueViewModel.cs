using Immowert4You.Application.Common.Constants;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Partners.Queries.GetPartners;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Properties;
using Immowert4You.Domain.Users;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Propertíes.Views.Increase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Increase
{
    public class IncreasePropertyValueViewModel : BaseViewModel
    {
        private UserDto _user;
        private PropertyDto _property;
        private double _bonusPrice;
        private bool _isPropertyAvailable;
        private double _newPrice;
        private ICommand _navigateToImprovementsPage;
        private readonly ITempStorage _tempStorage;
        private readonly IGetPartnersQuery _getPartnersQuery;

        public IncreasePropertyValueViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage,
            IGetPropertiesQuery getPropertiesQuery,
            IGetCurrentUserQuery getCurrentUserQuery,
            IGetPartnersQuery getPartnersQuery) : base(busyManager, navigationService)
        {
            Header = "Ihre Immowert Steigerung";

            _tempStorage = tempStorage;
            _getPartnersQuery = getPartnersQuery;

            getCurrentUserQuery.GetUserExecuted += RefreshUser;
            getPropertiesQuery.UserPropertiesExecuted += RefreshProperty;
        }

        private void RefreshUser(object sender, GetUserEventArgs e)
        {
            User = e.User;
        }

        private async void RefreshProperty(object sender, List<PropertyDto> properties)
        {
            Property ??= (properties?.FirstOrDefault());

            if (Property is null)
                return;

            await _busyManager.SetBusy();

            try
            {
                var partners = await _getPartnersQuery.Execute();

                _tempStorage.Save(partners);
            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                await _busyManager.SetUnBusy();
            }
        }

        public UserDto User
        {
            get => _user;
            set
            {
                RiseAndSetIfChanged(ref _user, value);

                RefreshAvailability();
            }
                
        }

        public PropertyDto Property
        {
            get => _property;
            set
            {
                NewPrice = _property is null ? value.Price : NewPrice;
                RiseAndSetIfChanged(ref _property, value);
            }
        }

        public bool IsPropertyAvailable
        {
            get => _isPropertyAvailable;
            set => RiseAndSetIfChanged(ref _isPropertyAvailable, value);
        }

        public void RefreshAvailability()
        {
            IsPropertyAvailable = User != null && User.PhoneNumberConfirmed;
        }

        public double NewPrice
        {
            get => _newPrice;
            set => RiseAndSetIfChanged(ref _newPrice, value);
        }

        public double Total
        {
            get => _bonusPrice;
            set
            {
                var bonusPrice = Property != null && Property.Price != 0 ? Property.Price * value / 1000 : _bonusPrice;

                NewPrice = Property.Price + bonusPrice;

                RiseAndSetIfChanged(ref _bonusPrice, bonusPrice);
            }
        }

        public ICommand NavigateToImprovementsPage => _navigateToImprovementsPage ??= new Command<string>(
            async (x) => await _navigationService.PushAsync<PropertyImprovmentsPage, ImpovmentGroup>(Constants.GetImprovments()[int.Parse(x)]));
    }
}
