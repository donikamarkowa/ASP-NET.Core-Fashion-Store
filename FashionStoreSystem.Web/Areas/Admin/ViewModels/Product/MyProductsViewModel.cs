using FashionStoreSystem.Web.ViewModels.Product;

namespace FashionStoreSystem.Web.Areas.Admin.ViewModels.Product
{
    public class MyProductsViewModel
    {
        public IEnumerable<ProductAllViewModel> AddedProducts { get; set; } = null!;

    }
}
