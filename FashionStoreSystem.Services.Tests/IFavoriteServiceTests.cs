using FashionStoreSystem.Services.Data;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Favorite;
using Microsoft.EntityFrameworkCore;
using static FashionStoreSystem.Services.Tests.DatabaseSeeder;


namespace FashionStoreSystem.Services.Tests
{
    public class IFavoriteServiceTests
    {
        private DbContextOptions<FashionStoreDbContext> dbOptions;
        private FashionStoreDbContext dbContext;

        private IFavoriteService favoriteService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<FashionStoreDbContext>()
               .UseInMemoryDatabase("FashionStoreInMemory")
               .Options;
            this.dbContext = new FashionStoreDbContext(this.dbOptions);

            SeedDatabase(this.dbContext);

            this.favoriteService = new FavoriteService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddToFavoriteAsyncShouldReturnTrueWhenProductAndUserExists()
        {
            string productId = NewProduct.Id.ToString();
            string userId = BuyerUser.Id.ToString();

            await this.favoriteService.AddToFavoriteAsync(productId, userId);
            int result = BuyerUser.FavoriteProducts.Count;

            Assert.AreEqual(1, result);     
        }

        [Test]
        public async Task IsProductInFavoriteByUserIdAsyncShouldReturnTrueWhenProductIsInFavorite()
        {
            string productId = NewProduct.Id.ToString();
            string userId = BuyerUser.Id.ToString();

            await this.favoriteService.AddToFavoriteAsync(productId, userId);
            bool result = await this.favoriteService.IsProductInFavoriteByUserIdAsync(productId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task RemoveFromFavoriteAsyncShouldReturnTrueWhenProductIsRemoved()
        {
            string productId = NewProduct.Id.ToString();
            string userId = BuyerUser.Id.ToString();
            await this.favoriteService.AddToFavoriteAsync(productId, userId);

            await this.favoriteService.RemoveFromFavoriteAsync(productId, userId);
            int favoriteProductsCount = BuyerUser.FavoriteProducts.Count();

            Assert.AreEqual(favoriteProductsCount, 0);

        }

        [Test]
        public async Task GetFavoriteByUserIdAsyncShouldReturnAllFavoriteProducts()
        {
            string productId = NewProduct.Id.ToString();
            string userId = BuyerUser.Id.ToString();
            await this.favoriteService.AddToFavoriteAsync(productId, userId);

            var result = await this.favoriteService.GetFavoriteByUserIdAsync(userId);
            int actualCount = result.Count();

            Assert.AreEqual(actualCount, 1);    
            
        }
    }
}
//public async Task<IEnumerable<FavoriteViewModel>> GetFavoriteByUserIdAsync(string userId)
//{
//    IEnumerable<FavoriteViewModel> allProductsInFavorite = await this.dbContext
//        .Favorites
//        .Include(f => f.Product)
//        .Where(f => f.UserId.ToString() == userId)
//        .Select(f => new FavoriteViewModel()
//        {
//            Id = f.Product.Id.ToString(),
//            Name = f.Product.Name,
//            Size = f.Product.Size,
//            ImageUrl = f.Product.ImageUrl,
//            Price = f.Product.Price
//        })
//        .ToArrayAsync();

//    return allProductsInFavorite;
//}

