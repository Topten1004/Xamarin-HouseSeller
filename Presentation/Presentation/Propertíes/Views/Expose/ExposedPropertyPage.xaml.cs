using Immowert4You.Presentation.Properties.Models.Result;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Expose
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExposedPropertyPage : ContentPage
	{
		public ExposedPropertyPage(ExposedPropertyViewModel userEstateRatingViewModel)
		{
			BindingContext = userEstateRatingViewModel;

			InitializeComponent ();
		}
    }
}