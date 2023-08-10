using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

using static FashionStoreSystem.Services.Tests.DatabaseSeeder;

namespace FashionStoreSystem.Services.Tests
{
    public class PurchaseServiceTests
    {
        private DbContextOptions<FashionStoreDbContext> dbOptions;
        private FashionStoreDbContext dbContext;

        private IPurchaseService purchaseService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<FashionStoreDbContext>()
               .UseInMemoryDatabase("FashionStoreInMemory")
               .Options;
            this.dbContext = new FashionStoreDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.purchaseService = new PurchaseService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AllAsyncShouldReturnAllPurchasesIfExists()
        {
            var purchases = await this.purchaseService.AllAsync();
            Assert.NotNull(purchases);
            Assert.AreEqual(1, purchases.Count()); 

            var purchaseViewModel = purchases.First();
            Assert.AreEqual("LADIES` SANDALS", purchaseViewModel.Name);
            Assert.AreEqual("https://static.reserved.com/media/catalog/product/7/5/7582S-00X-010-1-706233_1.jpg", purchaseViewModel.ImageUrl);
            Assert.AreEqual("Andrea Andreova", purchaseViewModel.SellerFullName);
            Assert.AreEqual("andrea123@sellers.com", purchaseViewModel.SellerEmail);
            Assert.AreEqual("Petur Petrov", purchaseViewModel.BuyerFullName);
            Assert.AreEqual("pesho@gmail.com", purchaseViewModel.BuyerEmail);
        }
    }
}
