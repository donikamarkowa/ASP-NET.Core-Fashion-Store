using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Web.ViewModels.User;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExistsByIdAsync(string userId);

        Task AddMoneyToWallet(string userId, decimal money);

        Task<decimal> GetWalletBalanceByUserIdAsync(string userId);

        Task<bool> UserHasEnoughMoneyToBuyProductAsync(string productId, string userId);

        Task<bool> IsProductInUserFavoriteAsync(string productId, string userId);

        Task UserBuyProductAsync(string productId, string userId);

        Task<string> GetFullNameByEmailAsync(string email);
        
        Task<string> GetFullNameByIdAsync(string userId);

        Task<IEnumerable<UserViewModel>> AllAsync();


    }
}
