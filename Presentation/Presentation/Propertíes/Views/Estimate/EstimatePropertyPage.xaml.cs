using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Estimate;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Estimate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EstimatePropertyPage : BaseContentPage
	{
        public EstimatePropertyPage(EstimatePropertyViewModel viewModel)
		{
			BindingContext = viewModel;

			InitializeComponent ();
		}
	}
}