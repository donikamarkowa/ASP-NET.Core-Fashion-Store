using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;
using static FashionStoreSystem.Services.Tests.DatabaseSeeder;
using System.Diagnostics.Contracts;

namespace FashionStoreSystem.Services.Tests
{
    public class UserServiceTests
    {
        private DbContextOptions<FashionStoreDbContext> dbOptions;
        private FashionStoreDbContext dbContext;

        private IUserService userService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<FashionStoreDbContext>()
               .UseInMemoryDatabase("FashionStoreInMemory")
               .Options;
            this.dbContext = new FashionStoreDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.userService = new UserService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddMoneyToWalletShouldAddMoneyToUsersWallet()
        {
            decimal moneyToAdd = 50.0m;
            string userId = BuyerUser.Id.ToString();

            var userWalletAmount = BuyerUser.Wallet;
            await this.userService.AddMoneyToWallet(userId, moneyToAdd);

            Assert.AreEqual(userWalletAmount + moneyToAdd, BuyerUser.Wallet);
        }

        [Test]
        public async Task AllAsyncShouldReturnAllUsers()
        {
            var users = await this.userService.AllAsync();

            Assert.NotNull(users);
            Assert.AreEqual(2, users.Count()); 

            var sellerUserViewModel = users.FirstOrDefault(u => u.Id == SellerUser.Id.ToString());
            var buyerUserViewModel = users.FirstOrDefault(u => u.Id == BuyerUser.Id.ToString());

            Assert.IsNotNull(sellerUserViewModel);
            Assert.IsNotNull(buyerUserViewModel);

        }

        [Test]
        public async Task GetFullNameByEmailAsyncShouldExists()
        {
            string email = "andrea123@sellers.com";

            var fullName = await this.userService.GetFullNameByEmailAsync(email);

            Assert.NotNull(fullName);
        }

        [Test]
        public async Task GetFullNameByIdAsyncShouldExists()
        {
            string userId = BuyerUser.Id.ToString();
            var fullName = await this.userService.GetFullNameByIdAsync(userId);

            Assert.NotNull(fullName);   
        }

        [Test]
        public async Task GetWalletBalanceByUserIdAsyncShouldExists()
        {
            string userId = BuyerUser.Id.ToString();

            var walletBalance = await this.userService.GetWalletBalanceByUserIdAsync(userId);

            Assert.GreaterOrEqual(walletBalance, 0);
        }

        [Test]
        public async Task IsProductInUserFavoriteAsyncShouldWokrdCorrect()
        {
            string userId = BuyerUser.Id.ToString();
            string productId = NewProduct.Id.ToString();

            var isInFavorite = await this.userService.IsProductInUserFavoriteAsync(productId, userId);

            Assert.IsFalse(isInFavorite);
        }

        [Test]
        public async Task UserExistsByIdAsyncShouldReturnTrueIfExists()
        {
            string userId = BuyerUser.Id.ToString();

            bool result = await this.userService.UserExistsByIdAsync(userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserHasEnoughMoneyToBuyProductAsyncShouldWorkCorrect()
        {
            string userId = BuyerUser.Id.ToString();
            string productId = NewProduct.Id.ToString();

            bool result = await this.userService.UserHasEnoughMoneyToBuyProductAsync(productId, userId);
            Assert.IsTrue(result);  
        }
    }
}
