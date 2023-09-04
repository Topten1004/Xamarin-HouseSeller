using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Commands.SendProperty;
using Immowert4You.Application.Properties.Commands.UploadPhoto;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Support;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Common.Validators;
using Immowert4You.Presentation.Properties.Views.Create;
using Immowert4You.Presentation.Propertíes.Views.Create.Modals;
using Immowert4You.Presentation.Support.Views;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertyAddressViewModel : BaseViewModel
    {
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly ISendPropertyCommand _sendPropertyCommand;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IGetPropertiesQuery _getPropertiesQuery;
        private readonly IUploadPhotoCommand _uploadPhotoCommand;
        private readonly IPropertyRepository _propertyRepository;

        private ValidatableObject<string> _postCode;
        private ValidatableObject<string> _location;
        private ValidatableObject<string> _street;
        private ValidatableObject<string> _houseNumber;

        private ICommand _navigateToUserOptions;
        private ICommand _navigateToUserPolicy;
        private ICommand _navigateToTerms;
        private string _checkBoxColor = "#0D62B0";
        private float _intentionToSell = 5;
        private bool _isCheckboxChecked;
        private bool _isBigCity;

        public PropertyAddressViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ITempStorage tempStorage,
            ICurrentUserRepository currentUserRepository,
            ISendPropertyCommand sendPropertyCommand,
            IGetCurrentUserQuery getCurrentUserQuery,
            IGetPropertiesQuery getPropertiesQuery,
            IUploadPhotoCommand uploadPhotoCommand,
            IPropertyRepository propertyRepository
            ) : base(busyManager, navigationService)
        {
            _currentUserRepository = currentUserRepository;
            _sendPropertyCommand = sendPropertyCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _getPropertiesQuery = getPropertiesQuery;
            _uploadPhotoCommand = uploadPhotoCommand;
            _propertyRepository = propertyRepository;

            Property = tempStorage.Read<PropertyDto>();
            Photos = tempStorage.Read<List<Photo>>();

            IsParcel = Property.Type == PropertyType.Parcel;

            Header = IsParcel ? "Teil 3/3" : "Teil 4/5";

            _postCode = new ValidatableObject<string>();
            _location = new ValidatableObject<string>();
            _street = new ValidatableObject<string>();
            _houseNumber = new ValidatableObject<string>();

           

            AddValidations();
        }

        public List<Photo> Photos { get; }

        public PropertyDto Property { get;}

        public bool IsParcel { get; }

        private void AddValidations()
        {
            _postCode.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte eine gültige Postleitzahl ein"
            });

            _location.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte den Ort an"
            });

            _street.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte die Straße an"
            });
        }

        public ValidatableObject<string> PostCode
        {
            get => _postCode;
            set => RiseAndSetIfChanged(ref _postCode, value);
        }

        public ValidatableObject<string> Location
        {
            get => _location;
            set => RiseAndSetIfChanged(ref _location, value);
        }
        public ValidatableObject<string> Street
        {
            get => _street;
            set => RiseAndSetIfChanged(ref _street, value);
        }
        public ValidatableObject<string> HouseNumber
        {
            get => _houseNumber;
            set => RiseAndSetIfChanged(ref _houseNumber, value);
        }
        public string CheckBoxColor
        {
            get => _checkBoxColor;
            set => RiseAndSetIfChanged(ref _checkBoxColor, value);
        }

        public float IntentionToSell
        {
            get => _intentionToSell;
            set => RiseAndSetIfChanged(ref _intentionToSell, value);
        }

        public bool IsBigCity
        {
            get => _isBigCity;
            set => RiseAndSetIfChanged(ref _isBigCity, value);
        }

        public bool IsCheckboxChecked
        {
            get => _isCheckboxChecked;
            set => RiseAndSetIfChanged(ref _isCheckboxChecked, value);
        }

        public ICommand NavigateToUserOptions => _navigateToUserOptions ??= new Command( 
            async () =>
            {
                
                await NavigateToUserOptionsExecute();  
            });

        public ICommand NavigateToUserPolicy => _navigateToUserPolicy ??= new Command(async () =>
        {
            var document = new HtmlDocument { Header = "Datenschutzerklärung für Nutzer", FileName = "policy_user.html" };
            await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
        });

        public ICommand NavigateToTerms => _navigateToTerms ??= new Command(async () =>
        {
            var document = new HtmlDocument { Header = "Bedingungen für Nutzer", FileName = "terms.html" };
            await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
        });

        private async Task NavigateToUserOptionsExecute()
        {

            if (IsCheckboxChecked == false)
            {
                CheckBoxColor = "#ff5252";
                return;
            }

            var updatedProperty = GetUpdatedProperty();

            if (_currentUserRepository.GetUser() != null)
            {
                if(await SaveExecute())
                    await _navigationService.PushAsync<PropertySummaryPage, PropertyDto>(updatedProperty);
                return;
            }

            await _navigationService.PushModalAsync<PhoneNumberModal, PropertyDto>(updatedProperty);
        }
        PropertyDto updatedProperty;
        private async Task<bool> SaveExecute()
        {
            if (!Validate())
                return false;

            await _busyManager.SetBusy();

            try
            {
                updatedProperty = GetUpdatedProperty();

                await _sendPropertyCommand.Execute(updatedProperty);

                await UploadPropertyPhotos();

                await _getCurrentUserQuery.Execute();

            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
                return false;
            }
            finally
            {
                await _busyManager.SetUnBusy();
            }
            return true;
        }

        private async Task UploadPropertyPhotos()
        {
            await _getPropertiesQuery.Execute();

            var property = _propertyRepository.GetProperties()?.FirstOrDefault();

            for (int i = 0; i < Photos.Count; i++)
            {
                var fileName = Photos[i].File.Path[(Photos[i].File.Path.LastIndexOf('/') + 1)..];

                var stream = Photos[i].File.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);

                await _uploadPhotoCommand.Execute(property.Id, bytes, fileName);

                await Task.Delay(1000);
            }
        }

        private bool Validate()
        {
            bool isValidPostCode = _postCode.Validate();
            bool isValidLocation = _location.Validate();
            bool isValidStreet = !IsBigCity || _street.Validate();
            bool isValidHouseNumber = !IsBigCity || _houseNumber.Validate();

            return isValidPostCode && isValidLocation && isValidStreet && isValidHouseNumber;
        }

        private PropertyDto GetUpdatedProperty()
        {
            Property.ZipCode = PostCode.Value;
            Property.City = Location.Value;
            Property.Address = Street.Value;
            Property.HouseNumber = HouseNumber.Value;
            Property.IntentionToSell = (int)Math.Round(IntentionToSell);

            return Property;
        }
    }
}