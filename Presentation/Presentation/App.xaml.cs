using Immowert4You.Application.Account.Commands.ConfirmPhone;
using Immowert4You.Application.Account.Commands.Login;
using Immowert4You.Application.Account.Commands.Register;
using Immowert4You.Application.Brokers.Commands;
using Immowert4You.Application.Brokers.GetBrokers;
using Immowert4You.Application.Chats.Commands.SendMessage;
using Immowert4You.Application.Chats.Queries.GetChats;
using Immowert4You.Application.Chats.Queries.GetOffers;
using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Partners.Queries.GetPartners;
using Immowert4You.Application.Properties.Commands.AcceptProperty;
using Immowert4You.Application.Properties.Commands.AddPropertyPrice;
using Immowert4You.Application.Propertíes.Commands.DenyProperty;
using Immowert4You.Application.Properties.Commands.SendProperty;
using Immowert4You.Application.Propertíes.Commands.UpdateProperty;
using Immowert4You.Application.Properties.Commands.UploadPhoto;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.TinyIoC;
using Immowert4You.Application.Users.Commands.AddPhoneNumber;
using Immowert4You.Application.Users.Commands.ConfirmPhoneNumber;
using Immowert4You.Application.Users.Commands.UpdateUser;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Presentation.Brokers.Models;
using Immowert4You.Presentation.Brokers.Views;
using Immowert4You.Presentation.Chats.Models;
using Immowert4You.Presentation.Chats.Views;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Customers.Views;
using Immowert4You.Presentation.Home.Views;
using Immowert4You.Presentation.Login.Models;
using Immowert4You.Presentation.Login.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using Immowert4You.Presentation.Properties.Models.Create.Extras;
using Immowert4You.Presentation.Propertíes.Models.Create.Modals;
using Immowert4You.Presentation.Properties.Models.Estimate;
using Immowert4You.Presentation.Properties.Models.Increase;
using Immowert4You.Presentation.Propertíes.Models.Increase;
using Immowert4You.Presentation.Properties.Models.Manage;
using Immowert4You.Presentation.Properties.Models.Result;
using Immowert4You.Presentation.Properties.Views.Create;
using Immowert4You.Presentation.Propertíes.Views.Create.Modals;
using Immowert4You.Presentation.Properties.Views.Estimate;
using Immowert4You.Presentation.Properties.Views.Expose;
using Immowert4You.Presentation.Properties.Views.Extras;
using Immowert4You.Presentation.Propertíes.Views.Increase;
using Immowert4You.Presentation.Properties.Views.Manage;
using Immowert4You.Presentation.Support.Models;
using Immowert4You.Presentation.Support.Views;
using Immowert4You.Service.Authentications;
using Immowert4You.Service.Brokers;
using Immowert4You.Service.Chats;
using Immowert4You.Service.Common.Client;
using Immowert4You.Service.Common.Url;
using Immowert4You.Service.Customers;
using Immowert4You.Service.Partners;
using Immowert4You.Service.Properties_;
using Immowert4You.Service.Users;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;

namespace Immowert4You.Presentation
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();
            RegisterViewModels();
            RegisterPages();
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };
        }

        protected override async void OnStart()
        {
            try
            {
                TinyIoCContainer.Current
                        .Resolve<INavigationService>()
                        .SetMainPage<BurgerPage>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RegisterServices()
        {
            TinyIoCContainer.Current.Register<ITokenRepository>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<ICurrentUserRepository>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<IBrokerRepository>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<IChatRepository>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<IPropertyRepository>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<ITempStorage>(ApplicationContext.Instance);
            TinyIoCContainer.Current.Register<INavigationService, NavigationService>();
            TinyIoCContainer.Current.Register<IApiUrlProvider, ApiUrlProvider>();
            TinyIoCContainer.Current.Register<IBusyManager, BusyManager>();
            TinyIoCContainer.Current.Register<IApiClient, ApiClient>();
            TinyIoCContainer.Current.Register<IAccountApiService, AccountApiService>();
            TinyIoCContainer.Current.Register<IPropertiesApiService, PropertiesApiService>();
            TinyIoCContainer.Current.Register<IUsersApiService, UsersApiService>();
            TinyIoCContainer.Current.Register<IBrokersApiService, BrokersApiService>();
            TinyIoCContainer.Current.Register<ICustomersApiService, CustomersApiService>();
            TinyIoCContainer.Current.Register<IChatsApiService, ChatsApiService>();
            TinyIoCContainer.Current.Register<IPartnersApiService, PartnersApiService>();
            TinyIoCContainer.Current.Register<ILoginCommand, LoginCommand>();
            TinyIoCContainer.Current.Register<IRegisterPhoneCommand, RegisterPhoneCommand>();
            TinyIoCContainer.Current.Register<IConfirmPhoneCommand, ConfirmPhoneCommand>();
            TinyIoCContainer.Current.Register<ISendPropertyCommand, SendPropertyCommand>();
            TinyIoCContainer.Current.Register<IGetCurrentUserQuery, GetCurrentUserQuery>();
            TinyIoCContainer.Current.Register<IAddPhoneNumberCommand, AddPhoneNumberCommand>();
            TinyIoCContainer.Current.Register<IConfirmPhoneNumberCommand, ConfirmPhoneNumberCommand>();
            TinyIoCContainer.Current.Register<IGetPropertiesQuery, GetPropertiesQuery>();
            TinyIoCContainer.Current.Register<IGetChatsQuery, GetChatsQuery>();
            TinyIoCContainer.Current.Register<ISendMessageCommand, SendMessageCommand>();
            TinyIoCContainer.Current.Register<IAcceptPropertyCommand, AcceptPropertyCommand>();
            TinyIoCContainer.Current.Register<IDenyPropertyCommand, DenyPropertyCommand>();
            TinyIoCContainer.Current.Register<IAddPropertyPriceCommand, AddPropertyPriceCommand>();
            TinyIoCContainer.Current.Register<IUploadPhotoCommand, UploadPhotoCommand>();
            TinyIoCContainer.Current.Register<IUpdatePropertyCommand, UpdatePropertyCommand>();
            TinyIoCContainer.Current.Register<IChangeBrokerActivityCommand, ChangeBrokerActivityCommand>();
            TinyIoCContainer.Current.Register<IGetBrokersQuery, GetBrokersQuery>();
            TinyIoCContainer.Current.Register<IUpdateUserCommand, UpdateUserCommand>();
            TinyIoCContainer.Current.Register<IGetOffersQuery, GetOffersQuery>();
            TinyIoCContainer.Current.Register<IGetPartnersQuery, GetPartnersQuery>();
        }
        private void RegisterViewModels()
        {
            TinyIoCContainer.Current.Register<LoginViewModel>();
            TinyIoCContainer.Current.Register<RegisterViewModel>();
            TinyIoCContainer.Current.Register<PropertyTypeViewModel>();
            TinyIoCContainer.Current.Register<PropertySubcategoryViewModel>();
            TinyIoCContainer.Current.Register<PropertyInfoViewModel>();
            TinyIoCContainer.Current.Register<PropertyRoomsViewModel>();
            TinyIoCContainer.Current.Register<PropertyExtraRoomsViewModel>();
            TinyIoCContainer.Current.Register<PropertyAddressViewModel>();
            TinyIoCContainer.Current.Register<PropertySummaryViewModel>();
            TinyIoCContainer.Current.Register<IncreasePropertyValueViewModel>();
            TinyIoCContainer.Current.Register<ExposedPropertyViewModel>();
            TinyIoCContainer.Current.Register<PendingPropertiesViewModel>();
            TinyIoCContainer.Current.Register<BrokerPointsViewModel>();
            TinyIoCContainer.Current.Register<ArchivedPropertiesViewModel>();
            TinyIoCContainer.Current.Register<TermsOfUseViewModel>();
            TinyIoCContainer.Current.Register<ChatViewModel>();
            TinyIoCContainer.Current.Register<ImprovementsViewModel>();
            TinyIoCContainer.Current.Register<HtmlDocumentViewModel>();
            TinyIoCContainer.Current.Register<PhoneNumberViewModel>();
        }
        private void RegisterPages()
        {
            TinyIoCContainer.Current.Register<LoginPage>();
            TinyIoCContainer.Current.Register<BrokerLoginPage>();
            TinyIoCContainer.Current.Register<RegisterPage>();
            TinyIoCContainer.Current.Register<UserTabbedPage>();
            TinyIoCContainer.Current.Register<PropertyTypePage>();
            TinyIoCContainer.Current.Register<PropertySubcategoryPage>();
            TinyIoCContainer.Current.Register<PropertyInfoPage>();
            TinyIoCContainer.Current.Register<PropertyRoomsPage>();
            TinyIoCContainer.Current.Register<PropertyExtraRoomsPage>();
            TinyIoCContainer.Current.Register<PropertyAddressPage>();
            TinyIoCContainer.Current.Register<PropertySummaryPage>();
            TinyIoCContainer.Current.Register<IncreasePropertyValuePage>();
            TinyIoCContainer.Current.Register<ExposedPropertyPage>();
            TinyIoCContainer.Current.Register<PendingPropertiesPage>();
            TinyIoCContainer.Current.Register<PointsForRatingsPage>();
            TinyIoCContainer.Current.Register<ArchivedPropertiesPage>();
            TinyIoCContainer.Current.Register<TermsOfUsePage>();
            TinyIoCContainer.Current.Register<ChatPage>();
            TinyIoCContainer.Current.Register<PropertyImprovmentsPage>();
            TinyIoCContainer.Current.Register<HtmlDocumentPage>();
            TinyIoCContainer.Current.Register<PhoneNumberModal>();
            TinyIoCContainer.Current.Register<TooManyPropertiesModal>();
        }
    }
}
