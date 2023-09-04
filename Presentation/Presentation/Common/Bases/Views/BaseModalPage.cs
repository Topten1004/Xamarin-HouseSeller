using Immowert4You.Presentation.Common.Bases.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Immowert4You.Presentation.Common.Bases.Views
{
    public class BaseModalPage : ContentPage
    {
        public BaseModalPage()
        {
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.OverFullScreen);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as BaseModalViewModel).OnModalAppearing();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await (BindingContext as BaseModalViewModel).OnModalDisappearing();
        }
    }
}
