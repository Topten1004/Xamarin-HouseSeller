using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Support;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Support.Models
{
    public class HtmlDocumentViewModel : BaseViewModel
    {
        public HtmlDocumentViewModel(IBusyManager busyManager, INavigationService navigationService, ITempStorage tempStorage) : base(busyManager, navigationService)
        {
            var document = tempStorage.Read<HtmlDocument>();

            Header = document.Header;

            Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(
                "Immowert4You.Presentation.Common.Assets." + document.FileName);

            using var reader = new StreamReader(stream);

            Html = reader.ReadToEnd();
        }

        public string Html { get; }
    }
}
