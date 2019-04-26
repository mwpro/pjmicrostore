using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Products.Photos.Controllers;

namespace Products.Photos.Storage
{
    public interface IPhotoStorage
    {
        Task<PhotoFileInfo> Save(IFormFile formFile);
    }
}