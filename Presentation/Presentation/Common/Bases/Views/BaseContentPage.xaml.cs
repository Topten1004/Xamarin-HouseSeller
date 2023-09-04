using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Common.Bases.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BaseContentPage : ContentPage
	{
		public BaseContentPage ()
		{
			On<iOS>().SetUseSafeArea(true);

			App.Current
				.On<Android>()
				.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

			InitializeComponent ();
		}
	}
}