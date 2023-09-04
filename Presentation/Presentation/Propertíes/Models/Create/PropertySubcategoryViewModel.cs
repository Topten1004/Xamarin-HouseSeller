using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Properties.Views.Create;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Properties.Models.Create
{
    public class PropertySubcategoryViewModel : BaseViewModel
    {
        private ICommand _navigateToHouseCondition;
        private Subcategory _selectedSubcategory;

        public PropertySubcategoryViewModel(
            IBusyManager busyManager, 
            INavigationService navigationService,
            ITempStorage tempStorage) : base(busyManager, navigationService)
        {
            Property = tempStorage.Read<PropertyDto>();

            Header = Property.Type == PropertyType.Parcel ? "Teil 1/3" : "Teil 1/5";

            Subcategories = GetSubcategories(Property.Type);
        }

        public PropertyDto Property { get; }

        public List<Subcategory> Subcategories { get; }

        public Subcategory SelectedSubcategory
        {
            get => _selectedSubcategory;
            set => RiseAndSetIfChanged(ref _selectedSubcategory, value);
        }

        public ICommand NavigateToHouseCondition => _navigateToHouseCondition ??= new Command(
            async () => await NavigateToHouseConditionExecute());


        //we can move it to Constants.Subcategories later
        private List<Subcategory> GetSubcategories(PropertyType subject)
        {
            return subject switch
            {
                PropertyType.House => new List<Subcategory>
                    {
                        new Subcategory { Icon = "house_1", Name = "Einfamilienhaus" },
                        new Subcategory { Icon = "apartament_house", Name = "Mehrfamilienhaus" },
                        new Subcategory { Icon = "terraced_houses", Name = "Reihenhaus" },
                    },
                PropertyType.Apartment => new List<Subcategory>
                    {
                        new Subcategory { Icon = "flat_1", Name = "Erdgeschosswohnung" },
                        new Subcategory { Icon = "flat_2", Name = "Etagenwohnung" },
                        new Subcategory { Icon = "flat_3", Name = "Dachgeschoss" },
                    },
                PropertyType.Parcel => new List<Subcategory>
                    {
                        new Subcategory { Icon = "house", Name = "Bauland" },
                        new Subcategory { Icon = "meadow", Name = "Grünland" },
                        new Subcategory { Icon = "factory", Name = "Gewerbegrund" },
                    },
                _ => throw new System.InvalidOperationException(),
            };
        }

        private async Task NavigateToHouseConditionExecute()
        {
            Property.SubCategory = SelectedSubcategory.Name;

            await _navigationService.PushAsync<PropertyInfoPage, PropertyDto>(Property);
        }
    }

    public class Subcategory
    {
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}
