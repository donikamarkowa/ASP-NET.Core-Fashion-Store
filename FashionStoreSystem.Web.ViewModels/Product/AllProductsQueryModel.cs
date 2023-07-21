using FashionStoreSystem.Web.ViewModels.Product.Enums;
using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.GeneralApplicationConstants;

namespace FashionStoreSystem.Web.ViewModels.Product
{
    public class AllProductsQueryModel
    {
        public AllProductsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.ProductsPerPage = EntitesPerPage;

            this.Categories = new HashSet<string>();
            this.Products = new HashSet<ProductAllViewModel>(); 
        }
        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Products By")]
        public ProductSorting ProductSorting { get; set; }
        public int CurrentPage { get; set; }

        [Display(Name = "Show Products On Page")]
        public int ProductsPerPage { get; set; }
        public int TotalProducts { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<ProductAllViewModel> Products { get; set; }

    }
}
