using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;

namespace Products.Catalog.Photos
{
    public interface IPhotoStorage
    {
        Task<PhotoFileInfo> Save(IFormFile formFile);
    }

    public class AzurePhotoStorage : IPhotoStorage
    {
        public async Task<PhotoFileInfo> Save(IFormFile formFile)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;");
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("productsphotos");
            var exists = await cloudBlobContainer.ExistsAsync();
            if (!exists)
                await cloudBlobContainer.CreateAsync();
            var blobReference = cloudBlobContainer.GetBlockBlobReference($"{Guid.NewGuid()}-{formFile.FileName}");   
            blobReference.UploadFromStream(formFile.OpenReadStream());

            return new PhotoFileInfo()
            {
                Uri = blobReference.Uri
            };
        }
    }

    public class PhotoFileInfo
    {
        public Uri Uri { get; set; }
    }

    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly PhotosContext _context;
        private readonly IPhotoStorage _photoStorage;

        public PhotosController(PhotosContext context, IPhotoStorage photoStorage)
        {
            _context = context;
            _photoStorage = photoStorage;
        }
        
        [HttpPost]
        [Route("/api/products/{productId}/photos")]
        public async Task<IActionResult> PostPhoto(int productId, IList<IFormFile> photos)
        {
            // todo validate file
            // todo create list and use addRange
            foreach (var photo in photos)
            {
                var uploadResult = await _photoStorage.Save(photo);
                _context.Photos.Add(new Photo()
                {
                    ProductId = productId,
                    OriginalUrl = uploadResult.Uri.ToString()
                });
            }

            await _context.SaveChangesAsync();

            // todo send events to resizer
            // todo so far search won't know about photos without further product update...

            return Accepted();
        }

        // todo reorder photos action

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeletePhoto(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return photo;
        }
    }
}
