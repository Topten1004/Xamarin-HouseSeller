using Immowert4You.Application.Chats.Queries.GetChats;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Support;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Login.Views;
using Immowert4You.Presentation.Support.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Home.Models
{
    public class BurgerViewModel : BaseViewModel
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IGetPropertiesQuery _getPropertiesQuery;
        private readonly IGetChatsQuery _getChatsQuery;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;

        public BurgerViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ITokenRepository tokenRepository,
            IGetPropertiesQuery getPropertiesQuery,
            IGetChatsQuery getChatsQuery,
            IGetCurrentUserQuery getCurrentUserQuery) : base(busyManager, navigationService)
        {
            _tokenRepository = tokenRepository;
            _getPropertiesQuery = getPropertiesQuery;
            _getChatsQuery = getChatsQuery;
            _getCurrentUserQuery = getCurrentUserQuery;

            if (IsUserLogged())
                FetchRepositories();
        }

        public List<BurgerMenuItem> MenuItems => new List<BurgerMenuItem>
        {
            new BurgerMenuItem { Title = "Nutzungsbedingungen",
                Command = new Command(async () => await _navigationService.PushAsync<TermsOfUsePage>())},

            new BurgerMenuItem { Title = "FAQs",
                Command = new Command(async () => await _navigationService.PushAsync<FaqPage>())},

            new BurgerMenuItem { Title = "Impressum",
                Command = new Command(async () =>
                {
                    var document = new HtmlDocument { Header = "Impressum", FileName = "impressum.html" };
                    await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
                })},

            new BurgerMenuItem { Title = "Datenschutz User",
                Command = new Command(async () =>
                {
                    var document = new HtmlDocument { Header = "Datenschutzerklärung für Nutzer", FileName = "policy_user.html" };
                    await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
                })},

            new BurgerMenuItem { Title = "Datenschutz Kooperationspartner",
                Command = new Command(async () =>
                {
                    var document = new HtmlDocument { Header = "Datenschutzerklärung für Kooperationspartner", FileName = "policy_partner.html" };
                    await _navigationService.PushAsync<HtmlDocumentPage, HtmlDocument>(document);
                })},

            new BurgerMenuItem { Title = "Kontakt",
                Command = new Command(async () => await _navigationService.PushAsync<ContactPage>())},

            new BurgerMenuItem { Title = "Bewerter",
                Command = new Command(async () => await _navigationService.PushAsync<BrokerLoginPage>())},
        };

        public string VersionInfo => $"Ver. {AppInfo.VersionString} ({AppInfo.BuildString})";

        private bool IsUserLogged()
        {
            var token = _tokenRepository.GetToken();

            return !string.IsNullOrEmpty(token);
        }

        private async void FetchRepositories()
        {
            await _busyManager.SetBusy();

            try
            {
                await _getCurrentUserQuery.Execute();
                await _getPropertiesQuery.Execute();
                await _getChatsQuery.Execute();
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

    public class BurgerMenuItem
    {
        public string Title { get; set; }
        public ICommand Command { get; set; }
    }
}
