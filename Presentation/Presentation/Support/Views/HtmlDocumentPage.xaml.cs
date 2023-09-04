using Immowert4You.Presentation.Support.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Support.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HtmlDocumentPage : ContentPage
    {
        public HtmlDocumentPage(HtmlDocumentViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}