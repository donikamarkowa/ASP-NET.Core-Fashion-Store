using FashionStoreSystem.Web.ViewModels.Home;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IndexViewModel>> TopThreeCheapestProductsAsync();
    }
}
