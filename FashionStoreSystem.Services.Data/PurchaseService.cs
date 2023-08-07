using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Purchase;
using FashionStoreSystem.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class PurchaseService : IPurchaseService
    {
        private readonly FashionStoreDbContext dbContext;

        public PurchaseService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<PurchaseViewModel>> AllAsync()
        {
            var allPurchases = await this.dbContext
                .Purchases
                .Select(p => new PurchaseViewModel
                {
                    Name = p.Product.Name,
                    ImageUrl = p.Product.ImageUrl,
                    SellerFullName = p.Product.Seller.FirstName + " " + p.Product.Seller.LastName,
                    SellerEmail = p.Product.Seller.User.Email,
                    BuyerFullName = p.User.FirstName + " " + p.User.LastName,
                    BuyerEmail = p.User.Email
                })
                .ToListAsync();

            return allPurchases;
        }
    }
}

