using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;
using static FashionStoreSystem.Services.Tests.DatabaseSeeder;

namespace FashionStoreSystem.Services.Tests
{
    public class SellerServiceTests
    {
        private  DbContextOptions<FashionStoreDbContext> dbOptions; 
        private  FashionStoreDbContext dbContext;

        private ISellerService sellerService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<FashionStoreDbContext>()
                .UseInMemoryDatabase("FashionStoreInMemory")
                .Options;
            this.dbContext = new FashionStoreDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.sellerService = new SellerService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SellerExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingSellerUserId = SellerUser.Id.ToString();

            bool result = await this.sellerService.SellerExistsByUserIdAsync(existingSellerUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task SellerExistsByPhoneNumberShouldReturnTrueWhenExists()
        {
            string existingSellerPhoneNumber = SellerUser.PhoneNumber;

            bool result = await this.sellerService.SellerExistsByPhoneNumberAsync(existingSellerPhoneNumber); 

            Assert.IsTrue(result);  
        }

        [Test]
        public async Task GetSellerIdByUserIdShouldReturnNotNullWhenExists()
        {
            string existingSellerId = SellerUser.Id.ToString();

            string sellerId = await this.sellerService.GetSellerIdByUserIdAsync(existingSellerId);

            Assert.NotNull(sellerId);
        }

        [Test]
        public async Task HasProductByIdAsyncShouldReturnTrueWhenHasProduct()
        {
            string existingSellerId = SellerUser.Id.ToString();
            string productId = NewProduct.Id.ToString();

            bool result = await this.sellerService.HasProductByIdAsync(existingSellerId, productId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasProductsByUserIdAsyncShouldReturnFalseWhenHasNoProcuts()
        {
            string existingSellerId = SellerUser.Id.ToString();

            bool result = await this.sellerService.HasProductsByUserIdAsync(existingSellerId);

            Assert.IsFalse(result);
        }
    }
}