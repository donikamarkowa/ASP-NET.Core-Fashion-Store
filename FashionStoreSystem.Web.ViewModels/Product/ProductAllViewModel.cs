using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FashionStoreSystem.Web.ViewModels.Product
{
    public class ProductAllViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
