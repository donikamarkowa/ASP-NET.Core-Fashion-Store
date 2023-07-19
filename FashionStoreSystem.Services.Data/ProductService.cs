using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly FashionStoreDbContext dbContext;
        public ProductService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<IndexViewModel>> TopThreeCheapestProductsAsync()
        {
            IEnumerable<IndexViewModel> topThreeProducts = await this.dbContext
                .Products
                .OrderBy(p => p.Price)
                .Take(3)
                .Select(p => new IndexViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToArrayAsync();

            return topThreeProducts;
        }
    }
}
