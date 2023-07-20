using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.EntityValidationConstants.Seller;

namespace FashionStoreSystem.Web.ViewModels.Seller
{
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(PhoneMaxLength, MinimumLength = PhoneMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
