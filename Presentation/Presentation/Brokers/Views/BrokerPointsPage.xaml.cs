using Immowert4You.Presentation.Brokers.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Brokers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PointsForRatingsPage : ContentPage
    {
        public PointsForRatingsPage(BrokerPointsViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}