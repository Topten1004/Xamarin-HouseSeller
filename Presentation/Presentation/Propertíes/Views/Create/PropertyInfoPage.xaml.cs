using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyInfoPage : BaseContentPage
    {
        public PropertyInfoPage(PropertyInfoViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}