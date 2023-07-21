using FashionStoreSystem.Data.Models;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Services.Data.Models.Product;
using FashionStoreSystem.Web.Data;
using FashionStoreSystem.Web.ViewModels.Home;
using FashionStoreSystem.Web.ViewModels.Product;
using FashionStoreSystem.Web.ViewModels.Product.Enums;
using FashionStoreSystem.Web.ViewModels.Seller;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly FashionStoreDbContext dbContext;
        public ProductService(FashionStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllProductsFilteredAndPagedServiceModel> AllAsync(AllProductsQueryModel queryModel)
        {
            IQueryable<Product> productsQuery = this.dbContext
                .Products
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                productsQuery = productsQuery
                    .Where(p => p.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                productsQuery = productsQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCard) ||
                                EF.Functions.Like(p.Size, wildCard) ||
                                EF.Functions.Like(p.Description, wildCard));
            }

            productsQuery = queryModel.ProductSorting switch
            {
                ProductSorting.PriceAscending => productsQuery
                    .OrderBy(p => p.Price),
                ProductSorting.PriceDescending => productsQuery
                    .OrderByDescending(p => p.Price),
                _ => productsQuery
                    .OrderBy(p => p.Name)
                    .ThenBy(p => p.SellerId)
            };

            IEnumerable<ProductAllViewModel> allProducts = await productsQuery
                .Where(p => p.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.ProductsPerPage)
                .Take(queryModel.ProductsPerPage)
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Size = p.Size,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                })
                .ToArrayAsync();

            int totalProducts = productsQuery.Count();

            return new AllProductsFilteredAndPagedServiceModel()
            {
                TotalProductsCount = totalProducts,
                Products = allProducts
            };
            
        }

        public async Task<IEnumerable<ProductAllViewModel>> AllBySellerIdAsync(string sellerId)
        {
            IEnumerable<ProductAllViewModel> allSellerProducts = await this.dbContext
                .Products
                .Where(p => p.IsActive 
                         && p.SellerId.ToString() == sellerId)
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Size = p.Size,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                })
                .ToArrayAsync();

            return allSellerProducts;
        }

        public async Task<IEnumerable<ProductAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<ProductAllViewModel> allUserBooks = await this.dbContext
               .Purchases
               .Include(p => p.Product)
               .Where(p => p.UserId.ToString() == userId)
               .Select(p => new ProductAllViewModel
               {
                   Id = p.Product.Id.ToString(),
                   Name = p.Product.Name,
                   Size = p.Product.Size,
                   ImageUrl = p.Product.ImageUrl,
                   Price = p.Product.Price,
               })
               .ToArrayAsync();

            return allUserBooks;
        }

        public async Task CreateAsync(ProductFormModel formModel, string sellerId)
        {
            Product newProduct = new Product()
            {
                Name = formModel.Name,
                Description = formModel.Description,
                Size = formModel.Size,
                ImageUrl = formModel.ImageUrl,
                Price = formModel.Price,
                CategoryId = formModel.CategoryId,
                SellerId = Guid.Parse(sellerId)
            };

            await this.dbContext.Products.AddAsync(newProduct);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ProductDetailsViewModel?> GetDetailsByIdAsync(string productId)
        {
            Product? product = await this.dbContext
                .Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ThenInclude(a => a.User)
                .Where(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.Id.ToString() == productId);

            if (product == null)
            {
                return null;
            }

            return new ProductDetailsViewModel()
            {
                Id = product.Id.ToString(),
                Size = product.Size,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Description = product.Description,
                Seller = new SellerInfoOnHouseViewModel()
                {
                    FirstName = product.Seller.FirstName,
                    LastName = product.Seller.LastName,
                    PhoneNumber = product.Seller.PhoneNumber,
                    Email = product.Seller.User.Email,
                }
            };
        }

        public async Task<IEnumerable<IndexViewModel>> TopThreeCheapestProductsAsync()
        {
            IEnumerable<IndexViewModel> topThreeProducts = await this.dbContext
                .Products
                .Where(p => p.IsActive)
                .OrderBy(p => p.Price)
                .Take(3)
                .Select(p => new IndexViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToArrayAsync();

            return topThreeProducts;
        }
    }
}
