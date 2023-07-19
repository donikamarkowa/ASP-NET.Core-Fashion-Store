using FashionStoreSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Web.Data
{
    public class FashionStoreDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public FashionStoreDbContext(DbContextOptions<FashionStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Purchase>()
                .HasKey(p => new {p.UserId, p.ProductId});  

            builder
                .Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .Entity<Purchase>()
               .HasOne(p => p.Product)
               .WithMany(u => u.Users)
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.ProductId });

            builder
                .Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.FavoriteProducts)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Favorite>()
                .HasOne(f => f.Product)
                .WithMany(p => p.FavoriteProducts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Category>()
                .HasData(new Category()
                {
                    Id = 1,
                    Name = "Dresses"
                },
                new Category()
                {
                    Id = 2,
                    Name = "T-shirts"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Trousers"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Jackets"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Jeans"
                },
                new Category()
                {
                    Id = 6,
                    Name = "Accessories"
                },
                new Category()
                {
                    Id = 7,
                    Name = "Shoes"
                });

            builder.Entity<Product>()
                .HasData(new Product()
                {
                    Name = "Mini Dress",
                    ImageUrl = "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg",
                    Description = "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.",
                    Size = "XS",
                    Price = 40,
                    CategoryId = 1,
                    SellerId = Guid.Parse("1BB98123-E199-4F38-8123-4A2E6B0F7814")
                });

            builder.Entity<Product>()
                .HasData(new Product()
                {
                    Name = "T-shirt boxy",
                    ImageUrl = "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg",
                    Description = "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.",
                    Size = "S",
                    Price = 13,
                    CategoryId = 2,
                    SellerId = Guid.Parse("1BB98123-E199-4F38-8123-4A2E6B0F7814")
                });

            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; init; } = null!;
        public DbSet<Product> Products { get; init; } = null!;
        public DbSet<Purchase> Purchases { get; init; } = null!;
        public DbSet<Seller> Sellers { get; init; } = null!;
        public DbSet<Favorite> Favorites { get; init; } = null!;
    }
}