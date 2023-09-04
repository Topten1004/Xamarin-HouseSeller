using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Presentation.Chats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Chats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        private List<Button> _carouselButtons;
        private List<StackLayout> _stacks;
        private readonly ICurrentUserRepository _currentUserRepository;

        public ChatPage(ChatViewModel viewModel, 
            ICurrentUserRepository currentUserRepository)
        {
            _currentUserRepository = currentUserRepository;

            BindingContext = viewModel;

            InitializeComponent();

            _carouselButtons = new List<Button> { btnCrsel1, btnCrsel2, btnCrsel3 };
            _stacks = new List<StackLayout> { chats, messages, offers };

            SetButtons();

            viewModel.OnMessageAdded += ChatRefreshing;

            chatsList.ItemSelected += (s, e) => 
            {
                chatsList.SelectedItem = null;
                btnCrsel_Clicked(btnCrsel2, null);
            };
        }

        private void SetButtons()
        {
            var viewModel = BindingContext as ChatViewModel;
            var user = _currentUserRepository.GetUser();

            if (user != null && user.IsBroker)
            {
                PaintButton(btnCrsel1);
                ShowStack(0);
            }
            else
            {
                btnCrsel1.IsVisible = false;
                PaintButton(btnCrsel2);
                ShowStack(1);
            }
        }

        private void btnCrsel_Clicked(object sender, EventArgs e)
        {
            foreach (var btn in _carouselButtons)
            {
                btn.BackgroundColor = Color.White;
                btn.TextColor = Color.DarkGray;
            }

            foreach (var stack in _stacks)
            {
                stack.IsVisible = false;
            }

            var clickedBtn = sender as Button;

            PaintButton(clickedBtn);

            ShowStack(_carouselButtons.IndexOf(clickedBtn));

        }

        private void PaintButton(Button button)
        {
            button.BackgroundColor = Color.FromHex("#0D62B0");
            button.TextColor = Color.White;
        }

        private void ShowStack(int buttonIndex)
        {
            _stacks[buttonIndex].IsVisible = true;
        }

        private void ChatRefreshing(object sender, EventArgs e)
        {
            var lastItem = chat.ItemsSource?.Cast<object>().LastOrDefault();

            chat.ScrollTo(lastItem, ScrollToPosition.End, false);
        }
    }
}