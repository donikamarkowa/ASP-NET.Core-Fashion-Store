using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data.Models.Statistics;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class StatisticsService : IStatisticsService
    {
        private readonly FashionStoreDbContext dbContext;
        public StatisticsService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel
            {
                TotalDresses = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Dresses"),
                TotalTshirts = await this.dbContext.Products.CountAsync(c => c.Category.Name == "T-shirts"),
                TotalTrousers = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Trousers"),
                TotalJackets = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Jackets"),
                TotalJeans = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Jeans"),
                TotalAccessories = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Accessories"),
                TotalShoes = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Shoes"),
                TotalSwimsuits = await this.dbContext.Products.CountAsync(c => c.Category.Name == "Swimsuit")
            };
        }
    }
}
