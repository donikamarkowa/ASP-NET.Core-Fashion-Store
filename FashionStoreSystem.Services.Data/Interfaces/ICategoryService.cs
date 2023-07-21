using FashionStoreSystem.Web.ViewModels.Category;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductSelectCategoryFormModel>> AllCategoriesAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
