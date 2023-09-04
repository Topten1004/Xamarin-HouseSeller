using Immowert4You.Presentation.Login.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrokerLoginPage : ContentPage
    {
        public BrokerLoginPage(LoginViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
             var address = "office@immowert4you.app";

            await Launcher.OpenAsync(new System.Uri($"mailto:{address}?subject=Ich habe mein Passwort vergessen"));
        }
    }
}