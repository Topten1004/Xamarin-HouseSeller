using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create.Extras;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Extras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyExtraRoomsPage : BaseContentPage
    {
        public PropertyExtraRoomsPage(PropertyExtraRoomsViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}