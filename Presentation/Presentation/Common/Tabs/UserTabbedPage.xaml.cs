using Immowert4You.Presentation.Customers.Views;
using Immowert4You.Presentation.Home.Views;
using Immowert4You.Presentation.Chats.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Immowert4You.Presentation.Properties.Views.Create;
using Immowert4You.Presentation.Properties.Views.Expose;

namespace Immowert4You.Presentation.Customers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : Xamarin.Forms.TabbedPage
    {
        public UserTabbedPage(
            PropertyTypePage homePage,
            ChatPage messagesListPage,
            ExposedPropertyPage userEstateRatingPage,
            IncreasePropertyValuePage increaseEstateValuePage)
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BarBackgroundColor = Color.White;

            this.Children.Add(homePage);
            this.Children.Add(messagesListPage);
            this.Children.Add(increaseEstateValuePage);
            this.Children.Add(userEstateRatingPage);

            InitializeComponent();
        }
    }
}