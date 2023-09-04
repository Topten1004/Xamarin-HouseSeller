using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Support.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FaqPage : BaseContentPage
    {
        public FaqPage(FaqViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }
    }
}