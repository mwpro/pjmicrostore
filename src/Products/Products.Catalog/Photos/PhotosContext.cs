using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Products.Catalog.Photos
{
    public class PhotosContext : DbContext
    {
        public PhotosContext(DbContextOptions<PhotosContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasKey(photo => new { photo.ProductId, photo.PhotoId });
            modelBuilder.Entity<Photo>()
                .Property(photo => photo.PhotoId).UseSqlServerIdentityColumn();
            modelBuilder.Entity<Photo>()
                .OwnsMany(photo => photo.Variants);

            modelBuilder.Entity<PhotoVariant>()
                .HasKey(variant => new { variant.ProductId, variant.PhotoId, variant.PhotoVariantId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Photo> Photos { get; set; }
    }

    public class Photo
    {
        public int ProductId { get; set; }
        public int PhotoId { get; set; }

        public string OriginalUrl { get; set; }
        public ICollection<PhotoVariant> Variants { get; set; }
    }

    public class PhotoVariant
    {
        public int ProductId { get; set; }
        public int PhotoId { get; set; }
        public int PhotoVariantId { get; set; }

        public string VariantName { get; set; }
        public string VariantUrl { get; set; }
    }
}
