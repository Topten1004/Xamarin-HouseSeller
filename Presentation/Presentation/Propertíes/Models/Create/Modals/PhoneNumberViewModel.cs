using Immowert4You.Application.Account.Commands.Register;
using Immowert4You.Application.Users.Commands.UpdateUser;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Presentation.Account.Models;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Common.Validators;
using Immowert4You.Presentation.Properties.Views.Create;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Propertíes.Models.Create.Modals
{
    public class PhoneNumberViewModel : BaseModalViewModel
    {
        private readonly IRegisterPhoneCommand _registerPhoneCommand;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IUpdateUserCommand _updateUserCommand;

        private string _checkBoxColor = "#0D62B0";
        private bool _isMan;
        private bool _isWoman;

        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _lastName;
        private ValidatableObject<string> _phoneNumber;

        private ICommand _saveAndExit;

        private string token = "";

        public PhoneNumberViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            IRegisterPhoneCommand registerPhoneCommand,
            IGetCurrentUserQuery getCurrentUserQuery,
            IUpdateUserCommand updateUserCommand) : base(busyManager, navigationService)
        {
            _registerPhoneCommand = registerPhoneCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _updateUserCommand = updateUserCommand;

            _firstName = new ValidatableObject<string>();
            _lastName = new ValidatableObject<string>();
            _phoneNumber = new ValidatableObject<string>();

            AddValidations();
        }

        private void AddValidations()
        {
            _firstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Geben Sie bitte den Vornamen an"
            });

            _lastName.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Geben Sie bitte den Nachnamen an"
            });

            _phoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Geben Sie bitte Ihre Tel. Nr. an"
            });
        }

        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set => RiseAndSetIfChanged(ref _firstName, value);
        }

        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set => RiseAndSetIfChanged(ref _lastName, value);
        }

        public ValidatableObject<string> PhoneNumber
        {
            get => _phoneNumber;
            set => RiseAndSetIfChanged(ref _phoneNumber, value);
        }

        public bool IsMan
        {
            get => _isMan;
            set
            {
                if (value) IsWoman = false;

                RiseAndSetIfChanged(ref _isMan, value);
            }
        }
        public bool IsWoman
        {
            get => _isWoman;
            set
            {
                if (value) IsMan = false;

                RiseAndSetIfChanged(ref _isWoman, value);
            }
        }
        public string CheckBoxColor
        {
            get => _checkBoxColor;
            set => RiseAndSetIfChanged(ref _checkBoxColor, value);
        }

        public ICommand SaveAndExit => _saveAndExit ??=
            new Command(async () => await SaveAndExitExecute());

        private async Task SaveAndExitExecute()
        {
            if (!Validate())
                return;

            if (await AddPhoneNumber())
            {
                await _navigationService.PopModalAsync();
            }
        }

        private async Task<bool> AddPhoneNumber()
        {
            await _busyManager.SetBusy();

            try
            {
                token = DependencyService.Get<IDeviceTokenManager>().GetDeviceToken().ToString();

                var gender = IsMan ? "male" : "female";
#if RELEASE
                string countryCode = "+43";
#else
                string countryCode = "+43"; //string countryCode = "+48";
#endif
                var phoneNumber = countryCode + _phoneNumber.Value;

                await _registerPhoneCommand.Execute(gender, _firstName.Value, _lastName.Value, phoneNumber);

                var user = await _getCurrentUserQuery.Execute();

                user.PhoneToken = token;//DependencyService.Get<IDeviceTokenManager>().GetDeviceToken();

                await _updateUserCommand.Execute(user);

                return true;
            }
            catch (Exception ex)
            {
                if(ex.Message.Equals("Task is not yet complete"))
                    await PopUpHelper.ShowAlert("Error", "Vorgang unvollständig");
                else
                    await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                await _busyManager.SetUnBusy();
            }

            return false;
        }


        private bool Validate()
        {
            bool isValidFirstName = _firstName.Validate();
            bool isValidLastName = _lastName.Validate();
            bool isValidPhoneNumber = _phoneNumber.Validate();

            if (!IsMan && !IsWoman)
            {
                CheckBoxColor = "#ff5252";
                return false;
            }

            return isValidFirstName && isValidLastName && isValidPhoneNumber;
        }
    }
}
