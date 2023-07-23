using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Favorite;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class FavoriteService : IFavoriteService
    {
        private readonly FashionStoreDbContext dbContext;
        public FavoriteService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddToFavoriteAsync(string productId, string userId)
        {
            Favorite newFavoriteProduct = new Favorite()
            {
                ProductId = Guid.Parse(productId),
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Favorites.AddAsync(newFavoriteProduct);
            await this.dbContext.SaveChangesAsync();    
        }

        public async Task<IEnumerable<FavoriteViewModel>> GetFavoriteByUserIdAsync(string userId)
        {
            IEnumerable<FavoriteViewModel> allProductsInFavorite = await this.dbContext
                .Favorites
                .Include(f => f.Product)
                .Where(f => f.UserId.ToString() == userId)
                .Select(f => new FavoriteViewModel()
                {
                    Id = f.Product.Id.ToString(),
                    Name = f.Product.Name,
                    Size = f.Product.Size,
                    ImageUrl = f.Product.ImageUrl,
                    Price = f.Product.Price
                })
                .ToArrayAsync();    

            return allProductsInFavorite;
        }

        public async Task<bool> IsProductInFavoriteByUserIdAsync(string productId, string userId)
        {
            bool result = await this.dbContext
                .Favorites
                .AnyAsync(f => f.UserId.ToString() == userId &&
                          f.ProductId.ToString() == productId);

            return result;
        }

        public async Task RemoveFromFavoriteAsync(string productId, string userId)
        {
            var removeFavoriteProduct = await this.dbContext
                .Favorites
                .Where(f => f.UserId.ToString() == userId &&
                       f.ProductId.ToString() == productId)
                .FirstOrDefaultAsync();

            dbContext.Favorites.Remove(removeFavoriteProduct!);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
