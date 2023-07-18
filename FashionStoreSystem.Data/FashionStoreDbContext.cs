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



            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; init; } = null!;
        public DbSet<Product> Products { get; init; } = null!;
        public DbSet<Purchase> Purchases { get; init; } = null!;
        public DbSet<Seller> Sellers { get; init; } = null!;
        public DbSet<Favorite> Favorites { get; init; } = null!;
    }
}