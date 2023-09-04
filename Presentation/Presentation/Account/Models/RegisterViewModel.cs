using Immowert4You.Application.Account.Commands.Register;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Common.Validators;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Login.Models
{
    public class RegisterViewModel : BaseViewModel
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _userSurname;
        private ValidatableObject<string> _userEmail;
        private ValidatableObject<string> _userPassword;
        private ValidatableObject<string> _userRepeatPassword;
        private ICommand _register;
        private readonly IRegisterPhoneCommand _registerUserCommand;

        public RegisterViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            IRegisterPhoneCommand registerUserCommand) : base(busyManager, navigationService)
        {
            _registerUserCommand = registerUserCommand;

            _userName = new ValidatableObject<string>();
            _userSurname = new ValidatableObject<string>();
            _userEmail = new ValidatableObject<string>();
            _userPassword = new ValidatableObject<string>();
            _userRepeatPassword = new ValidatableObject<string>();
            
            AddValidations();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userName.Validations.Add(new MaxLengthRule(length: 150)
            {
                ValidationMessage = "Der Name darf 150 Zeichen nicht überschreiten."
            });
            _userSurname.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userSurname.Validations.Add(new MaxLengthRule(length: 150)
            {
                ValidationMessage = "Der Nachname darf 150 Zeichen nicht überschreiten."
            });
            _userEmail.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userEmail.Validations.Add(new EmailRule<string>());
            _userPassword.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userPassword.Validations.Add(new MaxLengthRule(length: 128)
            {
                ValidationMessage = "Das Passwort darf 128 Zeichen nicht überschreiten."
            });
            _userPassword.Validations.Add(new MinLengthRule(length: 8)
            {
                ValidationMessage = "Das Passwort muss mindestens 8 Zeichen enthalten."
            });
            _userPassword.Validations.Add(new LowerUpperNumberSpecialRule()
            {
                ValidationMessage = "Das Passwort sollte ein kleines Zeichen, ein großes Zeichen, eine Zahl und ein Symbol haben"
            });
            
            AddRepeatPasswordValidation();
        }

        public ValidatableObject<string> UserName
        {
            get => _userName;
            set => RiseAndSetIfChanged(ref _userName, value);
        }
        public ValidatableObject<string> UserSurname
        {
            get => _userSurname;
            set => RiseAndSetIfChanged(ref _userSurname, value);
        }

        public string SelectedSex { get; set; }

        public ValidatableObject<string> UserEmail
        {
            get => _userEmail;
            set
            {
                value.Value?.Trim();
                RiseAndSetIfChanged(ref _userEmail, value);
            }
        }
        public ValidatableObject<string> UserPassword
        {
            get => _userPassword;
            set => RiseAndSetIfChanged(ref _userPassword, value);
        }
        public ValidatableObject<string> UserRepeatPassword
        {
            get => _userRepeatPassword;
            set => RiseAndSetIfChanged(ref _userRepeatPassword, value);
        }

        public ICommand Register => _register ??= new Command(RegisterAsync);

        public async void RegisterAsync()
        {
            if (!Validate())
            {
                return;
            }

            await _busyManager.SetBusy();

            try
            {
                //await _registerUserCommand.Execute(UserName.Value, UserSurname.Value, UserEmail.Value, UserPassword.Value);

                //await PopUpHelper.ShowAlert("", "Klicken Sie auf den Link, den wir an die von Ihnen angegebene E-Mail-Adresse gesendet haben, um Ihr Konto zu bestätigen.");

                await _navigationService.PopAsync();
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

        private void AddRepeatPasswordValidation()
        {
            _userRepeatPassword.Validations.Clear();
            _userRepeatPassword.Validations.Add(new IsNotNullOrEmptyRule<string>());
            _userRepeatPassword.Validations.Add(new MatchPasswordRule()
            {
                PasswordToMatch = _userPassword.Value
            });
        }

        private bool Validate()
        {
            bool isValidUserName = _userName.Validate();
            bool isValidUserSurname = _userSurname.Validate();
            bool isValidEmail = _userEmail.Validate();
            bool isValidPassword = _userPassword.Validate();

            // Hack validate repeated password, this way password to match against is updated
            AddRepeatPasswordValidation();
            bool isValidRepeatPassword = _userRepeatPassword.Validate();

            return isValidUserName && isValidUserSurname && isValidEmail
                && isValidPassword && isValidRepeatPassword;
        }
    }
}
