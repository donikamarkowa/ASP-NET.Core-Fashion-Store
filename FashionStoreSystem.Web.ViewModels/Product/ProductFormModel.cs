using FashionStoreSystem.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.EntityValidationConstants.Product;

namespace FashionStoreSystem.Web.ViewModels.Product
{
    public class ProductFormModel
    {
        public ProductFormModel()
        {
            this.Categories = new HashSet<ProductSelectCategoryFormModel>();    
        }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(SizeMaxLength, MinimumLength = SizeMinLength)]
        public string Size { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<ProductSelectCategoryFormModel> Categories { get; set; }
    }
}
