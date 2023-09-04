using Immowert4You.Application.Common.Constants;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Partners;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Propertíes.Models.Increase
{
    public class ImprovementsViewModel : BaseViewModel
    {
        private ICommand _visitWebside;

        public ImprovementsViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage,
            IPropertyRepository propertyRepository) : base(busyManager, navigationService)
        {
            ImprovmentGroup = tempStorage.Read<ImpovmentGroup>();

            var partners = tempStorage.Read<IEnumerable<PartnerDto>>();

            var property = propertyRepository.GetProperties()?.First();

            Partner = partners
                .FirstOrDefault(partner => partner.ZipCode.Contains(property.ZipCode) &&
                ImprovmentGroup.Header == partner.Profession);
        }

        public ImpovmentGroup ImprovmentGroup { get; }
        public PartnerDto Partner { get; }

        public ICommand VisitWebsite => _visitWebside ??=
            new Command(async () => await Browser.OpenAsync(Partner.WebsiteURL));
    }
}
