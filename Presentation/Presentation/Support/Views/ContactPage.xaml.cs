using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Support.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Support.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : BaseContentPage
    {
        public ContactPage(ContactViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}