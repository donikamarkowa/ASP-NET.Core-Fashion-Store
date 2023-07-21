using FashionStoreSystem.Web.ViewModels.Product;

namespace FashionStoreSystem.Services.Data.Models.Product
{
    public class AllProductsFilteredAndPagedServiceModel
    {
        public AllProductsFilteredAndPagedServiceModel()
        {
            this.Products = new HashSet<ProductAllViewModel>();
        }
        public int TotalProductsCount { get; set; }
        public IEnumerable<ProductAllViewModel> Products { get; set; }
    }
}
