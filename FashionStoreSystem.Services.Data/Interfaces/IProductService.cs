using FashionStoreSystem.Services.Data.Models.Product;
using FashionStoreSystem.Web.ViewModels.Home;
using FashionStoreSystem.Web.ViewModels.Product;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> TopThreeCheapestProductsAsync();

        Task CreateAsync(ProductFormModel formModel, string SellerId);

        Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductsQueryModel queryModel);
    }
}
