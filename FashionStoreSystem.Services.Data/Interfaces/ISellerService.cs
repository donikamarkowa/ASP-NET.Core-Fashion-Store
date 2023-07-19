namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);
    }
}
