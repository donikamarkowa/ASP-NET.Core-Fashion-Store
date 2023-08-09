using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Web.Data;

namespace FashionStoreSystem.Services.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser SellerUser;
        public static Seller Seller;
        public static Product NewProduct;
        public static ApplicationUser BuyerUser;

        public static void SeedDatabase(FashionStoreDbContext dbContext)
        {
            SellerUser = new ApplicationUser()
            {
                UserName = "andrea123@sellers.com",
                NormalizedUserName = "ANDREA123@SELLERS.COM",
                Email = "andrea123@sellers.com",
                NormalizedEmail = "ANDREA123@SELLERS.COM",
                EmailConfirmed = true,
                PasswordHash = "f33ae3bc9a22cd7564990a794789954409977013966fb1a8f43c35776b833a95",
                SecurityStamp = "269056af-a8f6-4112-a085-1f6d69655052",
                ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
                TwoFactorEnabled = false,
                FirstName = "Andrea",
                LastName = "Andreova",
                PhoneNumber = "+35989765455555"
            };
            Seller = new Seller()
            {
                FirstName = "Andrea",
                LastName = "Andreova",
                PhoneNumber = "+35989765455555",
                User = SellerUser
            };
            NewProduct = new Product()
            {
                Name = "LADIES` SANDALS",
                Description = "The sandals have a straight toe and an ankle fastening",
                ImageUrl = "https://static.reserved.com/media/catalog/product/7/5/7582S-00X-010-1-706233_1.jpg",
                Price = 80,
                CategoryId = 7,
                Seller = Seller,
                Size = "38"
            };
            BuyerUser = new ApplicationUser()
            {
                UserName = "pesho@gmail.com",
                NormalizedUserName = "PESHO@GMAIL.COM",
                Email = "pesho@gmail.com",
                NormalizedEmail = "PESHO@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
                ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
                TwoFactorEnabled = false,
                FirstName = "Petur",
                LastName = "Petrov",
                PhoneNumber = "+359888888888",
                Wallet = 300
            };

            dbContext.Users.Add(SellerUser);
            dbContext.Sellers.Add(Seller);
            dbContext.Products.Add(NewProduct);
            dbContext.Users.Add(BuyerUser);

            dbContext.SaveChanges();
        }
    }
}
