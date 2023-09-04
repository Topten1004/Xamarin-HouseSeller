using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create.Extras;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Extras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyExtraInfoPage : BaseContentPage
    {
        public PropertyExtraInfoPage(PropertyExtraInfoViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}