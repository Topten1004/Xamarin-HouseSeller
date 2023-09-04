using Immowert4You.Application.Chats.Commands.SendMessage;
using Immowert4You.Application.Chats.Queries.GetChats;
using Immowert4You.Application.Chats.Queries.GetOffers;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Chats;
using Immowert4You.Domain.Properties;
using Immowert4You.Domain.Users;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Chats.Models
{
    public class ChatViewModel : BaseViewModel
    {
        private string _chatEntryText;
        private ICommand _sendMessage;
        private bool _isChatRefreshing;
        private ObservableCollection<ChatDto> _chats;
        private ObservableCollection<MessageDto> _messages;
        private ObservableCollection<OfferDto> _offers;
        private ChatDto _selectedChat;
        private UserDto _user;
        private bool _isChatVisible;
        private readonly IGetChatsQuery _getChatsQuery;
        private readonly ISendMessageCommand _sendMessageCommand;
        private readonly IGetOffersQuery _getOffersQuery;

        public event EventHandler OnMessageAdded;

        public ChatViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            IGetChatsQuery getChatsQuery,
            ISendMessageCommand sendMessageCommand,
            IGetOffersQuery getOffersQuery,
            IGetCurrentUserQuery getCurrentUserQuery,
            IGetPropertiesQuery getPropertiesQuery) : base(busyManager, navigationService)
        {
            _getChatsQuery = getChatsQuery;
            _sendMessageCommand = sendMessageCommand;
            _getOffersQuery = getOffersQuery;

            Header = "Postfach";

            getCurrentUserQuery.GetUserExecuted += RefreshUser;
            getPropertiesQuery.UserPropertiesExecuted += RefreshChat;
            getPropertiesQuery.BrokerPropertiesExecuted += RefreshChat;
        }

        private void RefreshChat(object sender, List<PropertyDto> properties)
        {
            if (properties.Any(p => p.IsEstimated))
            {
                IsChatVisible = true;
                LoadChatsOnScreen();
                LoadOffersOnScreen();
            }
        }

        private void RefreshUser(object sender, GetUserEventArgs e)
        {
            User = e.User;
        }

        public bool IsChatVisible
        {
            get => _isChatVisible;
            set => RiseAndSetIfChanged(ref _isChatVisible, value);
        }

        public UserDto User
        {
            get => _user;
            set => RiseAndSetIfChanged(ref _user, value);
        }

        public ChatDto SelectedChat
        {
            get => _selectedChat;
            set
            {
                if (value is null)
                    return;

                Messages = new ObservableCollection<MessageDto>(value.Messages);

                RiseAndSetIfChanged(ref _selectedChat, value);
            }
        }

        public ObservableCollection<ChatDto> Chats
        {
            get => _chats;
            set
            {
                if (value != null)
                    SelectedChat = value.FirstOrDefault();

                RiseAndSetIfChanged(ref _chats, value);
            }
        }

        public ObservableCollection<MessageDto> Messages
        {
            get => _messages;
            set
            {
                foreach (var mes in value)
                {
                    mes.IsOwnMessage = mes.Author == User.Email;
                }

                RiseAndSetIfChanged(ref _messages, value);
            }
        }

        public ObservableCollection<OfferDto> Offers
        {
            get => _offers;
            set => RiseAndSetIfChanged(ref _offers, value);
        }

        public bool IsChatRefreshing
        {
            get => _isChatRefreshing;
            set => RiseAndSetIfChanged(ref _isChatRefreshing, value);
        }

        public string ChatEntryText
        {
            get => _chatEntryText;
            set => RiseAndSetIfChanged(ref _chatEntryText, value);
        }

        public ICommand SendMessage => _sendMessage ??=
            new Command(async () => await SendMessageExecute());


        private async void LoadChatsOnScreen()
        {
            IsChatRefreshing = true;

            try
            {
                var chats = await _getChatsQuery.Execute();

                Chats = new ObservableCollection<ChatDto>(chats);
            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                IsChatRefreshing = false;
            }
        }

        private async void LoadOffersOnScreen()
        {
            IsChatRefreshing = true;

            try
            {
                var offers = await _getOffersQuery.Execute();

                Offers = new ObservableCollection<OfferDto>(offers);
            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                IsChatRefreshing = false;
            }
        }

        private async Task SendMessageExecute()
        {
            if (string.IsNullOrEmpty(ChatEntryText)) return;

            try
            {
                await _sendMessageCommand.Execute(SelectedChat.Id, ChatEntryText);

                AddHappyMessage(ChatEntryText);

                OnMessageAdded?.Invoke(this, null);

                ChatEntryText = "";
            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
        }

        private void AddHappyMessage(string text)
        {
            Messages.Add(new MessageDto { Text = text, IsOwnMessage = true });
        }
    }
}
