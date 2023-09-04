using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Propertíes.Models.Create.Modals;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Propertíes.Views.Create.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TooManyPropertiesModal : BaseModalPage
    {
        public TooManyPropertiesModal(TooManyPropertiesViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}