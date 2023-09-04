using Immowert4You.Presentation.Home.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyTypePage : ContentPage
    {
        public PropertyTypePage(PropertyTypeViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();

            App.Current
                .On<Xamarin.Forms.PlatformConfiguration.Android>()
                .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }
        private void BurgerClicked(object sender, EventArgs e)
        {
            var burger = Parent as BurgerPage;

            burger.IsPresented = true;
        }
    }
}