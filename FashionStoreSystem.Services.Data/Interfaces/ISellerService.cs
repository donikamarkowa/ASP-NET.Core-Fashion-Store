using FashionStoreSystem.Web.ViewModels.Seller;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);

        Task<bool> SellerExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasProductsByUserIdAsync(string userId);

        Task Create(string userId, BecomeSellerFormModel model);

        Task<string?> GetSellerIdByUserIdAsync(string userId);

        Task<bool> HasProductByIdAsync(string userId, string productId);
    }
}
