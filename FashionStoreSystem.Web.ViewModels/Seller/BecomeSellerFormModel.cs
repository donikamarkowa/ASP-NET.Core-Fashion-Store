using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.EntityValidationConstants.Seller;

namespace FashionStoreSystem.Web.ViewModels.Seller
{
    public class BecomeSellerFormModel
    {
        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(PhoneMaxLength, MinimumLength = PhoneMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
