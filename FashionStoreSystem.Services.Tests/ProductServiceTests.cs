using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;
using static FashionStoreSystem.Services.Tests.DatabaseSeeder;
using FashionStoreSystem.Web.ViewModels.Product;
using FashionStoreSystem.Web.ViewModels.Home;
using FashionStoreSystem.Web.ViewModels.Seller;

namespace FashionStoreSystem.Services.Tests
{
    public class ProductServiceTests
    {
        private DbContextOptions<FashionStoreDbContext> dbOptions;
        private FashionStoreDbContext dbContext;

        private IProductService productService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<FashionStoreDbContext>()
               .UseInMemoryDatabase("FashionStoreInMemory")
               .Options;
            this.dbContext = new FashionStoreDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.productService = new ProductService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AllBySellerIdAsyncShouldReturnAllSellersProducts()
        {
            string existingSellerId = Seller.UserId.ToString();
            var result = await this.productService.AllBySellerIdAsync(existingSellerId);
            int actualCount = result.Count();

            int expectedCount = dbContext.Products.Count(p => p.SellerId == Guid.Parse(existingSellerId));
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public async Task AllByUserIdAsyncShouldReturnAllUsersProducts()
        {
            string existingBuyerId = BuyerUser.Id.ToString();
            var result = await this.productService.AllByUserIdAsync(existingBuyerId);
            int actualCount = result.Count();

            Assert.AreEqual(1, actualCount);
        }

        [Test]
        public async Task GetProductForDeleteByIdAsyncShouldReturnProductIfExists()
        {
            string productId = NewProduct.Id.ToString();

            var result = await this.productService.GetProductForDeleteByIdAsync(productId);
            Assert.IsInstanceOf(typeof(ProductPreDeleteDetailsViewModel), result);
            Assert.AreEqual(NewProduct.Name, result.Name);
        }

        [Test]
        public async Task DeleteProductByIdAsyncShouldDeleteGivenProduct()
        {
            string productId = NewProduct.Id.ToString();
            await this.productService.DeleteProductByIdAsync(productId);

            Assert.False(NewProduct.IsActive);
        }

        [Test]
        public async Task IsBoughtByUserIdAsyncShouldReturnTrue()
        {
            string userId = BuyerUser.Id.ToString();
            string productId = NewProduct.Id.ToString();

            bool result = await this.productService.IsBoughtByUserIdAsync(productId, userId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsSellerWithIdOwnerOfProductWithIdAsyncShouldExists()
        {
            string sellerId = Seller.Id.ToString();
            string productId = NewProduct.Id.ToString();

            bool result = await this.productService.IsSellerWithIdOwnerOfProductWithIdAsync(productId, sellerId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TopThreeCheapestProductsAsyncShouldExists()
        {
            var result = await this.productService.TopThreeCheapestProductsAsync();

            Assert.AreEqual(result.Count(), 1);
        }


    }
}

