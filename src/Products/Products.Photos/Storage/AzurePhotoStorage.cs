using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Products.Photos.Controllers;

namespace Products.Photos.Storage
{
    public class AzurePhotoStorage : IPhotoStorage
    {
        private readonly IConfiguration _configuration;

        public AzurePhotoStorage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PhotoFileInfo> Save(IFormFile formFile)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_configuration.GetValue<string>("PhotoStorageConnectionString"));
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
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
}