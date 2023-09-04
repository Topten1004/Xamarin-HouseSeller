using Immowert4You.Presentation.Properties.Models.Estimate;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Estimate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingPropertiesPage : ContentPage
    {
        public PendingPropertiesPage(PendingPropertiesViewModel viewModel)
        {
            BindingContext = viewModel;
            
            InitializeComponent();

            propertiesList.ItemSelected += (s, e) => { propertiesList.SelectedItem = null; };
        }
    }
}