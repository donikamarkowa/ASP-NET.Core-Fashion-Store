using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;

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

        public Task RemoveFromFavoriteAsync(string productId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
