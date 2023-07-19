using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class SellerService : ISellerService
    {
        private readonly FashionStoreDbContext dbContext;
        public SellerService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> SellerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Sellers
                .AnyAsync(s => s.UserId.ToString() == userId);

            return result;
        }
    }
}
