using FashionStoreSystem.Web.ViewModels.Purchase;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseViewModel>> AllAsync();
    }
}
