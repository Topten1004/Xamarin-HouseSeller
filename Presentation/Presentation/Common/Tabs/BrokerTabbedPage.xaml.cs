using Immowert4You.Presentation.Chats.Views;
using Immowert4You.Presentation.Properties.Views.Manage;
using Immowert4You.Presentation.Properties.Views.Estimate;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Brokers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrokerTabbedPage : Xamarin.Forms.TabbedPage
    {
        public BrokerTabbedPage(
            ArchivedPropertiesPage archivedRatingsPage,
            PointsForRatingsPage pointsForRatingsPage,
            PendingPropertiesPage ratingRequestsPage,
            ChatPage messagesListPage)
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            BarBackgroundColor = Color.White;

            this.Children.Add(ratingRequestsPage);
            this.Children.Add(messagesListPage);
            this.Children.Add(pointsForRatingsPage);
            this.Children.Add(archivedRatingsPage);

            InitializeComponent();
        }
    }
}