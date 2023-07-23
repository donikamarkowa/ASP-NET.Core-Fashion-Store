using System.ComponentModel.DataAnnotations;

namespace FashionStoreSystem.Web.ViewModels.Favorite
{
    public class FavoriteViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
