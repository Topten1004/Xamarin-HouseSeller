using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Immowert4You.Presentation.Common.Mocked;
using Immowert4You.Presentation.Common.Mocked.Models;
using System.Collections.Generic;

namespace Immowert4You.Presentation.Properties.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyAddressPage : BaseContentPage
    {
        private List<string> _bigCititesPlzs;
        public PropertyAddressPage(PropertyAddressViewModel viewModel)
        {
            BindingContext = viewModel;
            _bigCititesPlzs = MockedDataManager.GetData<BigCitiesModel>("BigCities").BigCitiesPlzs;
            InitializeComponent();
        }
        //send read message confirmation
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            (BindingContext as PropertyAddressViewModel).IsBigCity = _bigCititesPlzs.Contains(e.NewTextValue);
        }
    }
}