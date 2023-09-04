using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Validators;
using Immowert4You.Presentation.Properties.Views.Create;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertyInfoViewModel : BaseViewModel
    {
        private ValidatableObject<string> _parcelSize;
        private ValidatableObject<string> _flatSize;
        private ValidatableObject<string> _year;
        private ValidatableObject<string> _roomNumber;
        private ValidatableObject<string> _floor;
        private ValidatableObject<string> _flatValue;

        private ICommand _navigateToHouseRooms;
        private float _conditionRate = 5;

        public PropertyInfoViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ITempStorage tempStorage) : base(busyManager, navigationService)
        {
            _flatSize = new ValidatableObject<string>();
            _parcelSize = new ValidatableObject<string>();
            _year = new ValidatableObject<string>();
            _roomNumber = new ValidatableObject<string>();
            _floor = new ValidatableObject<string>();
            _flatValue = new ValidatableObject<string>();

            AddValidations();

            Property = tempStorage.Read<PropertyDto>();

            Header = Property.Type == PropertyType.Parcel ? "Teil 2/3" : "Teil 2/5";
        }

        private void AddValidations()
        {
            _flatSize.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte Ihre Wohnfläche an"
            });
            _flatSize.Validations.Add(new ParsableToFloatRule<string>());

            _parcelSize.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Geben Sie bitte Ihre Grundstücksgröße an"
            });
            _parcelSize.Validations.Add(new ParsableToFloatRule<string>());

            _year.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte das Baujahr an"
            });
            _year.Validations.Add(new ParsableToIntRule<string>());

            _roomNumber.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Geben sie bitte die Zimmeranzahl Ihrer Immobilie an"
            });
            _roomNumber.Validations.Add(new ParsableToIntRule<string>());

            _floor.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "In welchem Stockwerk befindet sich Ihre Wohnung?"
            });
            _floor.Validations.Add(new ParsableToIntRule<string>());

            _flatValue.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "In welchem Stockwerk befindet sich Ihre Wohnung?"
            });
            _flatValue.Validations.Add(new ParsableToIntRule<string>());
        }

        public PropertyDto Property { get; }

        public bool IsHouse => Property.Type == PropertyType.House;
        public bool IsFlat => Property.Type == PropertyType.Apartment;
        public bool IsParcel => Property.Type == PropertyType.Parcel;
        public bool IsNotGrassland => Property.Type == PropertyType.Parcel && Property.SubCategory != "Grünland";
        public bool IsFloorAvailable => Property.Type == PropertyType.Apartment && Property.SubCategory != "Erdgeschosswohnung";

        public ValidatableObject<string> ParcelSize
        {
            get => _parcelSize;
            set => RiseAndSetIfChanged(ref _parcelSize, value);
        }

        public ValidatableObject<string> FlatSize
        {
            get => _flatSize;
            set => RiseAndSetIfChanged(ref _flatSize, value);
        }

        public ValidatableObject<string> Year
        {
            get => _year;
            set => RiseAndSetIfChanged(ref _year, value);
        }

        public ValidatableObject<string> RoomNumber
        {
            get => _roomNumber;
            set => RiseAndSetIfChanged(ref _roomNumber, value);
        }

        public ValidatableObject<string> Floor
        {
            get => _floor;
            set => RiseAndSetIfChanged(ref _floor, value);
        }
        public ValidatableObject<string> Value
        {
            get => _flatValue;
            set => RiseAndSetIfChanged(ref _flatValue, value);
        }

        public float ConditionRate
        {
            get => _conditionRate;
            set => RiseAndSetIfChanged(ref _conditionRate, value);
        }

        public ICommand NavigateToHouseRooms => _navigateToHouseRooms ??= new Command(
            async () => await NavigateToHouseRoomsExecute());

        private async Task NavigateToHouseRoomsExecute()
        {
            if (!Validate())
                return;

            var updatedProperty = GetUpdatedProperty();

            if (Property.Type == PropertyType.Parcel)
                await _navigationService.PushAsync<PropertyAddressPage, PropertyDto>(updatedProperty);
            else
                await _navigationService.PushAsync<PropertyRoomsPage, PropertyDto>(updatedProperty);
        }

        private bool Validate()
        {
            bool isValidFlatSize = IsParcel || _flatSize.Validate();
            bool isValidParcelSize = IsFlat || _parcelSize.Validate();
            bool isValidYear = IsParcel || _year.Validate();
            bool isValidRoomNumber = IsParcel || _roomNumber.Validate();
            bool isValidFloor = !IsFloorAvailable || _floor.Validate();

            return isValidYear && isValidRoomNumber && isValidFlatSize && isValidParcelSize && isValidFloor;
        }

        private PropertyDto GetUpdatedProperty()
        {
            Property.Size = IsFlat ? default : float.Parse(ParcelSize.Value);
            Property.LivingSurface = IsParcel ? default : float.Parse(FlatSize.Value);
            Property.YearOfBuilt = IsParcel ? default : int.Parse(Year.Value);
            Property.Rooms = IsParcel ? default : int.Parse(RoomNumber.Value);
            Property.Floor = !IsFloorAvailable ? default : int.Parse(Floor.Value);
            Property.ConditionRate = (int)ConditionRate;

            return Property;
        }
    }
}
