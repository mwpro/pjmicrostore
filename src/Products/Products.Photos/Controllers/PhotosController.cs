using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Photos.Contracts.Events;
using Products.Photos.Domain;
using Products.Photos.Storage;

namespace Products.Photos.Controllers
{
    public class PhotoFileInfo
    {
        public Uri Uri { get; set; }
    }

    [Authorize]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly PhotosContext _context;
        private readonly IPhotoStorage _photoStorage;
        private readonly IBus _bus;

        public PhotosController(PhotosContext context, IPhotoStorage photoStorage, IBus bus)
        {
            _context = context;
            _photoStorage = photoStorage;
            _bus = bus;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/products/{productId}/photos")]
        public IActionResult GetPhotos(int productId)
        {
            var productPhotos = _context.Photos.Where(x => x.ProductId == productId).ToList();
            if (!productPhotos.Any())
                return NotFound();

            return Ok(productPhotos);
        }

        [HttpPost]
        [Authorize(AuthorizationPolicies.AdminOnly)]
        [Route("/api/products/{productId}/photos")]
        public async Task<IActionResult> PostPhoto(int productId, IList<IFormFile> photos)
        {
            // todo validate file
            // todo create list and use addRange
            foreach (var photo in photos)
            {
                var uploadResult = await _photoStorage.Save(photo);
                var photoEntry = new Photo()
                {
                    ProductId = productId,
                    OriginalUrl = uploadResult.Uri.ToString()
                };
                _context.Photos.Add(photoEntry);


                await _context.SaveChangesAsync();
                await _bus.Publish(new PhotoAddedEvent()
                {
                    OriginalUrl = photoEntry.OriginalUrl,
                    PhotoId = photoEntry.PhotoId,
                    ProductId = photoEntry.ProductId
                });
            }
            
            // todo send events to resizer

            return Accepted();
        }

        // todo reorder photos action

        // DELETE: api/Photos/5
        [Authorize(AuthorizationPolicies.AdminOnly)]
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

            await _bus.Publish(new PhotoRemovedEvent()
            {
                OriginalUrl = photo.OriginalUrl,
                PhotoId = photo.PhotoId,
                ProductId = photo.ProductId
            });


            return photo;
        }
    }
}
