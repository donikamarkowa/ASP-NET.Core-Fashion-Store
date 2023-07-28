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
                TotalDresses = await this.dbContext.Categories.CountAsync(c => c.Name == "Dresses"),
                TotalTshirts = await this.dbContext.Categories.CountAsync(c => c.Name == "T-shirts"),
                TotalTrousers = await this.dbContext.Categories.CountAsync(c => c.Name == "Trousers"),
                TotalJackets = await this.dbContext.Categories.CountAsync(c => c.Name == "Jackets"),
                TotalJeans = await this.dbContext.Categories.CountAsync(c => c.Name == "Jeans"),
                TotalAccessories = await this.dbContext.Categories.CountAsync(c => c.Name == "Accessories"),
                TotalShoes = await this.dbContext.Categories.CountAsync(c => c.Name == "Shoes")
            };
        }
    }
}
