using FashionStoreSystem.Data.Models;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExistsByIdAsync(string userId);

        Task AddMoneyToWallet(string userId, decimal money);

        Task<decimal> GetWalletBalanceByUserIdAsync(string userId); 

    }
}
