using Immowert4You.Application.Brokers.Commands;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Commands.AcceptProperty;
using Immowert4You.Application.Propertíes.Commands.DenyProperty;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Domain.Brokers;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Brokers.Views;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Properties.Views.Estimate;
using Immowert4You.Presentation.Properties.Views.Manage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Estimate
{
    public class PendingPropertiesViewModel : BaseViewModel
    {
        private readonly IAcceptPropertyCommand _acceptPropertyCommand;
        private readonly IBrokerRepository _brokerRepository;
        private readonly IDenyPropertyCommand _denyPropertyCommand;
        private readonly IChangeBrokerActivityCommand _changeBrokerActivityCommand;
        private ICommand _navigateToPoints;
        private ICommand _navigateToArchive;
        private ICommand _acceptProperty;
        private ICommand _rejectProperty;
        private ObservableCollection<PropertyDto> _properties;
        private bool _isActive;

        public PendingPropertiesViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            IAcceptPropertyCommand acceptPropertyCommand,
            IGetPropertiesQuery getPropertiesQuery,
            IBrokerRepository brokerRepository,
            IDenyPropertyCommand denyPropertyCommand,
            IChangeBrokerActivityCommand changeBrokerActivityCommand) : base(busyManager, navigationService)
        {
            Header = "Ihre offenen Bewertungen";

            _acceptPropertyCommand = acceptPropertyCommand;
            _brokerRepository = brokerRepository;
            _denyPropertyCommand = denyPropertyCommand;
            _changeBrokerActivityCommand = changeBrokerActivityCommand;

            getPropertiesQuery.BrokerPropertiesExecuted += GetPropertiesQuery_Executed;

            IsActive = _broker.IsActive;
        }

        private void GetPropertiesQuery_Executed(object sender, List<PropertyDto> properties)
        {
            Properties = new ObservableCollection<PropertyDto>(properties?
                .Where(p => !p.IsEstimated && !p.IsDenied)?
                .ToList());
        }

        private BrokerDto _broker => _brokerRepository.GetBroker();

        public ObservableCollection<PropertyDto> Properties
        {
            get => _properties;
            set => RiseAndSetIfChanged(ref _properties, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value != _broker.IsActive)
                {
                    _changeBrokerActivityCommand.Execute(value);

                    _broker.IsActive = value;

                    _brokerRepository.SetBroker(_broker);
                }

                RiseAndSetIfChanged(ref _isActive, value);
            }
        }

        public ICommand NavigateToPoints => _navigateToPoints ??= new Command(
            async () => await _navigationService.PushAsync<PointsForRatingsPage>());

        public ICommand NavigateToArchive => _navigateToArchive ??= new Command(
            async () => await _navigationService.PushAsync<ArchivedPropertiesPage>());

        public ICommand AcceptProperty => _acceptProperty ??= new Command<PropertyDto>(
            async (dto) => await AcceptPropertyExecute(dto));

        public ICommand RejectProperty => _rejectProperty ??= new Command<PropertyDto>(
            async (dto) => await RejectPropertyExecute(dto));

        private async Task RejectPropertyExecute(PropertyDto dto)
        {
            var success = await PopUpHelper.GetUserDecision(
                "", "Sind Sie sicher, dass Sie diese Bewertungsanfrage Ablehnen wollen?");

            if (success)
                Properties.Remove(dto);

            await _busyManager.SetBusy();

            try
            {
                await _denyPropertyCommand.Execute(dto.Id);
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

        private async Task AcceptPropertyExecute(PropertyDto dto)
        {
            await _busyManager.SetBusy();

            try
            {
                if (!dto.IsAgreed)
                    await _acceptPropertyCommand.Execute(dto.Id);
                dto.IsAgreed = true;
                await _navigationService.PushAsync<EstimatePropertyPage, PropertyDto>(dto);
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
}
