using Immowert4You.Application.Account.Commands.Login;
using Immowert4You.Application.Support;
using Immowert4You.Application.Users.Commands.UpdateUser;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Presentation.Account.Models;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Common.Validators;
using Immowert4You.Presentation.Home.Views;
using Immowert4You.Presentation.Support.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Login.Models
{
    public class LoginViewModel : BaseViewModel
    {
        private ValidatableObject<string> _userEmail;
        private ValidatableObject<string> _userPassword;
        private readonly ILoginCommand _loginUserCommand;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IUpdateUserCommand _updateUserCommand;
        private ICommand _navigateToUserPolicy;
        private ICommand _login;
        private string _checkBoxColor = "#0D62B0";
        private bool _isBroker;
        private bool _isCheckboxChecked;

        public LoginViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ILoginCommand loginUserCommand,
            IGetCurrentUserQuery getCurrentUserQuery,
            IUpdateUserCommand updateUserCommand) : base(busyManager, navigationService)
        {
            _loginUserCommand = loginUserCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _updateUserCommand = updateUserCommand;
            _userEmail = new ValidatableObject<string>();
            _userPassword = new ValidatableObject<string>();
            AddValidations();
        }

        public ValidatableObject<string> UserEmail
        {
            get => _userEmail;
            set => RiseAndSetIfChanged(ref _userEmail, value);
        }

        public ValidatableObject<string> UserPassword
        {
            get => _userPassword;
            set => RiseAndSetIfChanged(ref _userPassword, value);
        }

        public bool IsBroker
        {
            get => _isBroker;
            set => RiseAndSetIfChanged(ref _isBroker, value);
        }

        public bool IsCheckboxChecked
        {
            get => _isCheckboxChecked;
            set => RiseAndSetIfChanged(ref _isCheckboxChecked, value);
        }

        public string CheckBoxColor
        {
            get => _checkBoxColor;
            set => RiseAndSetIfChanged(ref _checkBoxColor, value);
        }

        public ICommand Login => _login ??=
            new Command(async () => await LoginExecute());

        private async Task LoginExecute()
        {
            if (!Validate())
            {
                return;
            }

            if (IsCheckboxChecked == false)
            {
                CheckBoxColor = "#ff5252";
                return;
            }
            await _busyManager.SetBusy();

            try
            {                
                await _loginUserCommand.Execute(_userEmail.Value, _userPassword.Value);

                var user = await _getCurrentUserQuery.Execute();
                user.PhoneToken = DependencyService.Get<IDeviceTokenManager>().GetDeviceToken();
                await _updateUserCommand.Execute(user);

                _navigationService.SetMainPage<BurgerPage>();
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

        public ICommand NavigateToUserPolicy => _navigateToUserPolicy ??= new Command(async () =>
        {
            var document = new HtmlDocument { Header = "Datenschutzerklärung für Kooperationspartner", FileName = "policy_partner.html" };
            await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
        });

        private void AddValidations()
        {
            _userEmail.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userEmail.Validations.Add(new EmailRule<string>());

            _userPassword.Validations.Add(new IsNotNullOrEmptyRule<string>());
        }

        private bool Validate()
        {
            bool isValidEmail = _userEmail.Validate();
            bool isValidPassword = _userPassword.Validate();

            return isValidEmail && isValidPassword;
        }
    }
}
