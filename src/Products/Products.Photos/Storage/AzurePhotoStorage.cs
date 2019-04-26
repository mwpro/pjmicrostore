using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Products.Photos.Controllers;

namespace Products.Photos.Storage
{
    public class AzurePhotoStorage : IPhotoStorage
    {
        public async Task<PhotoFileInfo> Save(IFormFile formFile)
        {
            // todo move to config
            var cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;");
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