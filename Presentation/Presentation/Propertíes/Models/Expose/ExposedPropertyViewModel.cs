using Immowert4You.Application.Account.Commands.ConfirmPhone;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Properties;
using Immowert4You.Domain.Users;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Result
{
    public class ExposedPropertyViewModel : BaseViewModel
    {
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IConfirmPhoneCommand _confirmPhoneCommand;
        private UserDto _user;
        private ICommand _sendVerificationCode;
        private string _code;
        private bool _isNotificationAvailable;
        private PropertyDto _property;
        private double _maxPropertyPrice;
        private double _minPropertyPrice;

        public ExposedPropertyViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            IGetCurrentUserQuery getCurrentUserQuery,
            IConfirmPhoneCommand confirmPhoneCommand,
            IGetPropertiesQuery getPropertiesQuery) : base(busyManager, navigationService)
        {
            Header = "Meine Bewertung";

            _getCurrentUserQuery = getCurrentUserQuery;
            _confirmPhoneCommand = confirmPhoneCommand;

            getCurrentUserQuery.GetUserExecuted += GetCurrentUserQuery_Executed;
            getPropertiesQuery.UserPropertiesExecuted += GetPropertiesQuery_Executed;
        }

        private void GetCurrentUserQuery_Executed(object sender, GetUserEventArgs e)
        {
            User = e.User;
        }

        private void GetPropertiesQuery_Executed(object sender, List<PropertyDto> properties)
        {
            Property = properties?.FirstOrDefault();

            IsVerificationAvailable = Property != null && Property.Price != 0;
        }

        public bool IsVerificationAvailable
        {
            get => _isNotificationAvailable;
            set => RiseAndSetIfChanged(ref _isNotificationAvailable, value);
        }

        public UserDto User
        {
            get => _user;
            set => RiseAndSetIfChanged(ref _user, value);
        }

        public PropertyDto Property
        {
            get => _property;
            set
            {
                if (value == null) return;

                MinPropertyPrice = value.Price * 0.85;
                MaxPropertyPrice = value.Price * 1.15;

                RiseAndSetIfChanged(ref _property, value);
            }
        }

        public double MinPropertyPrice
        {
            get => _minPropertyPrice;
            set => RiseAndSetIfChanged(ref _minPropertyPrice, value);
        }

        public double MaxPropertyPrice
        {
            get => _maxPropertyPrice;
            set => RiseAndSetIfChanged(ref _maxPropertyPrice, value);
        }

        public string Code
        {
            get => _code;
            set => RiseAndSetIfChanged(ref _code, value);
        }

        public ICommand SendVerificationCode => _sendVerificationCode ??= new Command(
            async () => await SendVerificationCodeExecute());

        private async Task SendVerificationCodeExecute()
        {
            await _busyManager.SetBusy();

            try
            {
                await _confirmPhoneCommand.Execute(Code);

                await _getCurrentUserQuery.Execute();
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
