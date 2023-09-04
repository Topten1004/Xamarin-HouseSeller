using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Propertíes.Commands.UpdateProperty;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Properties.Views.Estimate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Manage
{
    public class ArchivedPropertiesViewModel : BaseViewModel
    {
        private ICommand _setHasNoContract;
        private ObservableCollection<PropertyDto> _properties;
        private ICommand _setHasMet;
        private ICommand _setHasContract;
        private ICommand _navigateToEstimatePage;
        private ArchiveMode _displayMode;
        private readonly IGetPropertiesQuery _getPropertiesQuery;
        private readonly IUpdatePropertyCommand _updatePropertyCommand;
        private readonly IPropertyRepository _propertyRepository;

        public ArchivedPropertiesViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            IGetPropertiesQuery getPropertiesQuery,
            IUpdatePropertyCommand updatePropertyCommand,
            IPropertyRepository propertyRepository) : base(busyManager, navigationService)
        {
            _getPropertiesQuery = getPropertiesQuery;
            _updatePropertyCommand = updatePropertyCommand;
            _propertyRepository = propertyRepository;

            Header = "Archiv";

            getPropertiesQuery.BrokerPropertiesExecuted += RefreshProperties;
        }

        private void RefreshProperties(object sender, List<PropertyDto> properties)
        {
            Properties = new ObservableCollection<PropertyDto>(GetArchivedProperties(_displayMode, properties));
        }

        public ArchiveMode DisplayMode
        {
            get => _displayMode;
            set
            {
                _displayMode = value;

                var properties = _propertyRepository.GetProperties();

                if (properties != null)
                    RefreshProperties(this, properties);
            } 
        }

        public ObservableCollection<PropertyDto> Properties
        {
            get => _properties;
            set => RiseAndSetIfChanged(ref _properties, value);
        }

        public ICommand NavigateToEstimatePage => _navigateToEstimatePage ??=
            new Command<PropertyDto>(async (x) => await _navigationService.PushAsync<EstimatePropertyPage, PropertyDto>(x));

        public ICommand SetHasContract => _setHasContract ??=
            new Command<PropertyDto>(async (x) => await SetHasContractExecute(x));

        public ICommand SetHasNoContract => _setHasNoContract ??=
            new Command<PropertyDto>(async (x) => await SetHasNoContractExecute(x));

        public ICommand SetHasMet => _setHasMet ??=
            new Command<PropertyDto>(async (x) => await SetHasMetExecute(x));

        private List<PropertyDto> GetArchivedProperties(ArchiveMode mode, List<PropertyDto> properties)
        {
            var estimatedProperties = properties.Where(p => p.IsEstimated).ToList();

            return mode switch
            {
                ArchiveMode.NEW => estimatedProperties?
                                       .Where(p => !p.IsClosed && !p.IsObsolete)?
                                       .ToList(),

                ArchiveMode.OBSOLETE => estimatedProperties?
                                      .Where(p => !p.IsClosed && p.IsObsolete)?
                                      .ToList(),

                ArchiveMode.CLOSED => estimatedProperties?
                                      .Where(p => p.IsClosed)?
                                      .ToList(),

                _ => new List<PropertyDto>(),
            };
        }

        private async Task SetHasContractExecute(PropertyDto property)
        {
            if (property.IsClosed)
                return;

            var success = await PopUpHelper.GetUserDecision(
               "Sind Sie sicher?", "Diese Eingabe kann nicht mehr Rückgängig gemacht werden.");

            if (success)
            {
                await _busyManager.SetBusy();

                try
                {
                    property.IsClosed = true;
                    property.HasContract = true;

                    await _updatePropertyCommand.Execute(property);

                    await _getPropertiesQuery.Execute();
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
        }

        private async Task SetHasNoContractExecute(PropertyDto property)
        {
            if (property.IsClosed)
                return;

            var success = await PopUpHelper.GetUserDecision(
               "Sind Sie sicher?", "Diese Eingabe kann nicht mehr Rückgängig gemacht werden.");

            if (success)
            {
                await _busyManager.SetBusy();

                try
                {
                    property.IsClosed = true;
                    property.HasContract = false;

                    await _updatePropertyCommand.Execute(property);

                    await _getPropertiesQuery.Execute();
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
        }

        private async Task SetHasMetExecute(PropertyDto property)
        {
            if (property.IsClosed)
                return;

            await _busyManager.SetBusy();

            try
            {
                property.HasMet = !property.HasMet;

                await _updatePropertyCommand.Execute(property);

                Properties.Remove(property);
                await Task.Delay(1);
                Properties.Add(property);

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
    }

    public enum ArchiveMode
    {
        NEW, OBSOLETE, CLOSED
    }
}
