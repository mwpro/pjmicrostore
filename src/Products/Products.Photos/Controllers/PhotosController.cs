using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Photos.Domain;
using Products.Photos.Storage;

namespace Products.Photos.Controllers
{
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
