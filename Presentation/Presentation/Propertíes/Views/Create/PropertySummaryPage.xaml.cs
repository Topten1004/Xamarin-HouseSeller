using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertySummaryPage : BaseContentPage
    {
        public PropertySummaryPage(PropertySummaryViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}