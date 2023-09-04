using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.UploadPhoto
{
    public class UploadPhotoCommand : IUploadPhotoCommand
    {
        private readonly IPropertiesApiService _propertiesApiService;

        public UploadPhotoCommand(
            IPropertiesApiService propertiesApiService,
            ITempStorage tempStorage)
        {
            _propertiesApiService = propertiesApiService;
        }
        public Task Execute(string propertyId, byte[] bytes, string fileName)
        {
            return _propertiesApiService.UploadPhoto(propertyId, bytes, fileName);
        }
    }
}
