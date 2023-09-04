using System.Threading.Tasks;

namespace Immowert4You.Application.Properties.Commands.UploadPhoto
{
    public interface IUploadPhotoCommand
    {
        Task Execute(string propertyId, byte[] bytes, string fileName);
    }
}
