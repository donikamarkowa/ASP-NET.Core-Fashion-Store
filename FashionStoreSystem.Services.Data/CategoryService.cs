using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly FashionStoreDbContext dbContext;
        public CategoryService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductSelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<ProductSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new ProductSelectCategoryFormModel
                {
                    Id = c.Id,
                    Name = c.Name, 
                })
                .ToArrayAsync();

            return allCategories;
        }
    }
}
