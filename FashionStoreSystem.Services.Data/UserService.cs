using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class UserService : IUserService
    {
        private readonly FashionStoreDbContext dbContext;

        public UserService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }
        public async Task AddMoneyToWallet(string userId, decimal money)
        {
            var user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            user.Wallet += money;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            ICollection<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email
                })
                .ToListAsync();

            foreach (UserViewModel u in allUsers)
            {
                Seller? seller = await this.dbContext
                    .Sellers
                    .FirstOrDefaultAsync(s => s.UserId.ToString() == u.Id);

                if (seller == null)
                {
                    u.PhoneNumber = string.Empty;
                }
                else
                {
                    u.PhoneNumber = seller.PhoneNumber;
                }
            }

            return allUsers;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<decimal> GetWalletBalanceByUserIdAsync(string userId)
        {
            var userWallet = await this.dbContext
                .Users
                .Where(u => u.Id.ToString() == userId)
                .Select(u => u.Wallet)
                .FirstOrDefaultAsync();

            return userWallet;
        }

        public async Task<bool> IsProductInUserFavoriteAsync(string productId, string userId)
        {
            var user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            var isProductInFavorite = user
                .FavoriteProducts
                .Any(fp => fp.ProductId.ToString() == productId);

            if (!isProductInFavorite)
            {
                return false;
            }

            return true;
        }

        public async Task UserBuyProductAsync(string productId, string userId)
        {
            var product = await this.dbContext
                .Products
                .FirstAsync(p => p.Id.ToString() == productId);

            var user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            user.Wallet -= product.Price;
            product.Seller.User.Wallet += product.Price; 

            Purchase purchase = new Purchase()
            {
                UserId = Guid.Parse(userId),
                ProductId = Guid.Parse(productId)
            };

            await this.dbContext.Purchases.AddAsync(purchase);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserExistsByIdAsync(string userId)
        { 
            bool result = await this.dbContext
                .Users
                .AnyAsync(u => u.Id.ToString() == userId);

            return result;
        }

        public async Task<bool> UserHasEnoughMoneyToBuyProductAsync(string productId, string userId)
        {
            var product = await this.dbContext
                .Products
                .FirstAsync(p => p.Id.ToString() == productId);

            var user = await this.dbContext
                .Users
                .FirstAsync(u => u.Id.ToString() == userId);

            if (product.Price < user.Wallet)
            {
                return true;
            }

            return false;
        }

        
    }
}
