using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Propertíes.Models.Increase;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Propertíes.Views.Increase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PropertyImprovmentsPage : BaseContentPage
	{
		public PropertyImprovmentsPage(
			ImprovementsViewModel viewModel)
		{
			BindingContext = viewModel;

			InitializeComponent ();
		}
	}
}