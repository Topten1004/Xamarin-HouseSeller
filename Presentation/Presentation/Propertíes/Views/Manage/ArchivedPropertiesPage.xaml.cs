using Immowert4You.Presentation.Properties.Models.Manage;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Manage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArchivedPropertiesPage : ContentPage
    {
        private List<Button> _carouselButtons;

        public ArchivedPropertiesPage(ArchivedPropertiesViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();

            _carouselButtons = new List<Button> { btnCrsel1, btnCrsel2, btnCrsel3 };

            PaintButton(btnCrsel1);
        }

        private void btnCrsel_Clicked(object sender, EventArgs e)
        {
            foreach (var btn in _carouselButtons)
            {
                btn.BackgroundColor = Color.White;
                btn.TextColor = Color.Gray;
            }

            var clickedBtn = sender as Button;

            PaintButton(clickedBtn);

        }

        private void PaintButton(Button button)
        {
            button.BackgroundColor = Color.FromHex("#0D62B0");
            button.TextColor = Color.White;

            var buttonIndex = _carouselButtons.IndexOf(button);

            var viewModel = BindingContext as ArchivedPropertiesViewModel;

            switch (buttonIndex)
            {
                case 0:
                    viewModel.DisplayMode = ArchiveMode.NEW;
                    break;
                case 1:
                    viewModel.DisplayMode = ArchiveMode.OBSOLETE;
                    break;
                case 2:
                    viewModel.DisplayMode = ArchiveMode.CLOSED;
                    break;
            }
        }
    }
}