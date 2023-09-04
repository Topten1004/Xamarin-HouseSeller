using Immowert4You.Presentation.Properties.Models.Increase;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Customers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncreasePropertyValuePage : ContentPage
    {
        private int total = 0;

        public IncreasePropertyValuePage(
            IncreasePropertyValueViewModel viewModel)        
        {
            ViewModel = viewModel;

            InitializeComponent();
        }

        private IncreasePropertyValueViewModel ViewModel
        {
            get => (BindingContext as IncreasePropertyValueViewModel);
            set => BindingContext = value;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 10 : -10;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }

        private void CheckBox_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 10 : -10;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }

        private void CheckBox_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 40 : -40;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }

        private void CheckBox_CheckedChanged_3(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 20 : -20;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }

        private void CheckBox_CheckedChanged_4(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 20 : -20;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }

        private void CheckBox_CheckedChanged_5(object sender, CheckedChangedEventArgs e)
        {
            total += e.Value ? 40 : -40;

            total_lbl.Text = total + "/140";

            ViewModel.Total = total;
        }
    }
}