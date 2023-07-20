using FashionStoreSystem.Web.ViewModels.Category;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductSelectCategoryFormModel>> AllCategoriesAsync();
    }
}
