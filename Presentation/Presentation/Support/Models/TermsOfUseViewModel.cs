using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;

namespace Immowert4You.Presentation.Support.Models
{
    public class TermsOfUseViewModel : BaseViewModel
    {
        public TermsOfUseViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService) : base(busyManager, navigationService)
        {
            Header = "Nutzungsbedingungen";
        }
    }
}
