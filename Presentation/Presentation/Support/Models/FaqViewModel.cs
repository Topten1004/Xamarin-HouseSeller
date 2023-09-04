using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Immowert4You.Presentation.Support.Models
{
    public class FaqViewModel : BaseViewModel
    {
        public FaqViewModel(
            IBusyManager busyManager,
            INavigationService navigationService) : base(busyManager, navigationService) 
        {
            Header = "FAQ's";
        }
    }
}
