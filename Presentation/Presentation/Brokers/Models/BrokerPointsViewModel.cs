using Immowert4You.Application.Brokers.GetBrokers;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Brokers;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using System;
using System.Collections.Generic;

namespace Immowert4You.Presentation.Brokers.Models
{
    public class BrokerPointsViewModel : BaseViewModel
    {
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IBrokerRepository _brokerRepository;
        private readonly IGetBrokersQuery _getBrokersQuery;
        private List<BrokerModel> _brokers = new List<BrokerModel>();
        private BrokerDto _broker;

        public BrokerPointsViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            IGetPropertiesQuery getPropertiesQuery,
            IGetCurrentUserQuery getCurrentUserQuery,
            IBrokerRepository brokerRepository,
            IGetBrokersQuery getBrokersQuery) : base(busyManager, navigationService)
        {
            Header = "Punkte";

            _getCurrentUserQuery = getCurrentUserQuery;
            _brokerRepository = brokerRepository;
            _getBrokersQuery = getBrokersQuery;

            getPropertiesQuery.BrokerPropertiesExecuted += GetPropertiesQuery_Executed;
        }

        public BrokerDto Broker
        {
            get => _broker;
            set => RiseAndSetIfChanged(ref _broker, value);
        }

        public List<BrokerModel> Brokers
        {
            get => _brokers;
            set => RiseAndSetIfChanged(ref _brokers, value);
        }

        private async void GetPropertiesQuery_Executed(object sender, List<PropertyDto> properties)
        {
            await _busyManager.SetBusy();

            try
            {
                await _getCurrentUserQuery.Execute();

                Broker = _brokerRepository.GetBroker();

                Header = $"Punkte {Broker.Points}";

                var brokerDtos = await _getBrokersQuery.Execute();

                var brokers = new List<BrokerModel>();

                var i = 0;

                foreach (var broker in brokerDtos)
                {
                    brokers.Add(new BrokerModel
                    {
                        Name = $"{broker.FirstName} {broker.LastName}",
                        Points = broker.Points
                    });

                    i++;

                    if (i == 3)
                        break;
                }

                brokers.Sort((x, y) => y.Points.CompareTo(x.Points));

                foreach (var broker in brokers)
                    broker.Place = brokers.IndexOf(broker) + 1;

                Brokers = brokers;
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

        public class BrokerModel
        {
            public int Place { get; set; }
            public string Name { get; set; }
            public int Points { get; set; }
        }
    }
}
