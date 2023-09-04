using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Application.Properties.Commands.SendProperty;
using Immowert4You.Application.Properties.Commands.UploadPhoto;
using Immowert4You.Application.Properties.Queries.GetProperties;
using Immowert4You.Application.Users.Queries;
using Immowert4You.Domain.Properties;
using Immowert4You.Presentation.Common.Bases.Models;
using Immowert4You.Presentation.Common.Services.Loading;
using Immowert4You.Presentation.Common.Services.Navigation;
using Immowert4You.Presentation.Common.Services.PopUps;
using Immowert4You.Presentation.Properties.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Propertíes.Models.Create.Modals
{
    public class PropertySentToBrokerViewModel : BaseModalViewModel
    {
        private readonly ISendPropertyCommand _sendPropertyCommand;
        private readonly IGetCurrentUserQuery _getCurrentUserQuery;
        private readonly IGetPropertiesQuery _getPropertiesQuery;
        private readonly IUploadPhotoCommand _uploadPhotoCommand;
        private readonly IPropertyRepository _propertyRepository;
        private Command _saveAndQuit;

        public PropertySentToBrokerViewModel(
            IBusyManager busyManager,
            INavigationService navigationService,
            ISendPropertyCommand sendPropertyCommand,
            ITempStorage tempStorage,
            IGetCurrentUserQuery getCurrentUserQuery,
            IGetPropertiesQuery getPropertiesQuery,
            IUploadPhotoCommand uploadPhotoCommand,
            IPropertyRepository propertyRepository) : base(busyManager, navigationService)
        {
            _sendPropertyCommand = sendPropertyCommand;
            _getCurrentUserQuery = getCurrentUserQuery;
            _getPropertiesQuery = getPropertiesQuery;
            _uploadPhotoCommand = uploadPhotoCommand;
            _propertyRepository = propertyRepository;

            Property = tempStorage.Read<PropertyDto>();
            Photos = tempStorage.Read<List<Photo>>();
        }

        public PropertyDto Property { get; }

        public List<Photo> Photos { get; }

        public ICommand SaveAndQuit => _saveAndQuit ??= new Command(
            async () => await SaveAndQuitExecute());

        private async Task SaveAndQuitExecute()
        {
            await _navigationService.PopModalAsync();

            await _busyManager.SetBusy();

            try
            {
                await _sendPropertyCommand.Execute(Property);

                await UploadPropertyPhotos();

                await _getCurrentUserQuery.Execute();

                await _navigationService.PopToRootAsync();
            }
            catch (Exception ex)
            {
                await PopUpHelper.ShowAlert("Error", ex.Message);
            }
            finally
            {
                await _busyManager.SetUnBusy();
            }
        }

        private async Task UploadPropertyPhotos()
        {
            await _getPropertiesQuery.Execute();

            var property = _propertyRepository.GetProperties()?.FirstOrDefault();

            for (int i = 0; i < Photos.Count; i++)
            {
                var fileName = Photos[i].File.Path[(Photos[i].File.Path.LastIndexOf('/') + 1)..];

                var stream = Photos[i].File.GetStream();
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);

                await _uploadPhotoCommand.Execute(property.Id, bytes, fileName);

                await Task.Delay(1000);
            }
        }
    }
}
