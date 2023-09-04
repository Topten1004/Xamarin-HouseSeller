using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Commands.AddPropertyPrice;
using Immowert4You.Application.Propertíes.Commands.UpdateProperty;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Estimate
{
    public class EstimatePropertyViewModel : BaseViewModel
    {
        private readonly IAddPropertyPriceCommand _addPropertyPriceCommand;
        private readonly IGetPropertiesQuery _getPropertiesQuery;
        private readonly IUpdatePropertyCommand _updatePropertyCommand;
        private ICommand _addTemplateMessage;
        private string _note;
        private ICommand _addPropertyPrice;
        private ICommand _updateNote;
        private int? _price;
        private ICommand _call;

        public EstimatePropertyViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ITempStorage tempStorage,
            IAddPropertyPriceCommand addPropertyPriceCommand,
            IUpdatePropertyCommand updatePropertyCommand,
            IGetPropertiesQuery getPropertiesQuery) : base(busyManager, navigationService)
        {
            Header = "Bewertungen";

            _addPropertyPriceCommand = addPropertyPriceCommand;
            _getPropertiesQuery = getPropertiesQuery;
            _updatePropertyCommand = updatePropertyCommand;        
            Property = tempStorage.Read<PropertyDto>();
            Note = Property.Note;
        }

        public PropertyDto Property { get; }

        public string PhoneNumber => Property.PhoneNumber;

        public int? Price
        {
            get => _price;
            set => RiseAndSetIfChanged(ref _price, value);
        }

        public string Note
        {
            get => _note;
            set => RiseAndSetIfChanged(ref _note, value);
        }

        public ICommand Call => _call ??=
            new Command(() => Xamarin.Essentials.PhoneDialer.Open(Property.PhoneNumber));

        public ICommand AddTemplateMessage => _addTemplateMessage ??=
            new Command(AddTemplateMessageExecute);

        public ICommand AddPropertyPrice => _addPropertyPrice ??=
            new Command(async () => await AddPropertyPriceExecute());

        public ICommand UpdateNote => _updateNote ??=
            new Command(async () => await UpdateNoteExecute());

        private async Task AddPropertyPriceExecute()
        {
            await _busyManager.SetBusy();

            try
            {
                if (!Price.HasValue)
                    return;

                await _addPropertyPriceCommand.Execute(Property.Id, Price.Value);

                await _getPropertiesQuery.Execute();

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
        }

        private async Task UpdateNoteExecute()
        {
            await _busyManager.SetBusy();

            try
            {
                Property.Note = Note;
                
                await _updatePropertyCommand.Execute(Property);

                await _getPropertiesQuery.Execute();

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
        }

        private void AddTemplateMessageExecute()
        {
            Note = "Da Sie nicht den genauen Standort Ihrer Immobilie eingeben " +
                $"haben, beruht dieser Indikative Marktwert auf den Durchschnittswert der {Property.Address} straße.";
        }
    }
}
