using System.ComponentModel.DataAnnotations;

namespace FashionStoreSystem.Web.ViewModels.Product
{
    public class ProductPreDeleteDetailsViewModel
    {
        public string Name { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
