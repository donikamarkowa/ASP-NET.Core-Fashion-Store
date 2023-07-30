using FashionStoreSystem.Services.Data.Models.Product;
using FashionStoreSystem.Web.ViewModels.Home;
using FashionStoreSystem.Web.ViewModels.Product;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> TopThreeCheapestProductsAsync();

        Task<string> CreateAndReturnIdAsync(ProductFormModel formModel, string SellerId);

        Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductsQueryModel queryModel);

        Task<IEnumerable<ProductAllViewModel>> AllByUserIdAsync(string userId);

        Task<IEnumerable<ProductAllViewModel>> AllBySellerIdAsync(string sellerId);

        Task<bool> ExistsByIdAsync(string productId);

        Task<ProductDetailsViewModel> GetDetailsByIdAsync(string productId);

        Task<ProductFormModel> GetProductForEditByIdAsync(string productId);

        Task<bool> IsSellerWithIdOwnerOfProductWithIdAsync(string productId, string sellerId);

        Task EditProductByIdAndFormModelAsync(string productId, ProductFormModel formModel);

        Task<ProductPreDeleteDetailsViewModel> GetProductForDeleteByIdAsync(string productId);

        Task DeleteProductByIdAsync(string productId);

        Task<bool> IsBoughtByUserIdAsync(string productId, string userId);

    }
}
