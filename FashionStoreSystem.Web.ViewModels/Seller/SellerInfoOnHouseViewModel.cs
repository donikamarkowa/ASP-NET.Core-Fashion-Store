using System.ComponentModel.DataAnnotations;

namespace FashionStoreSystem.Web.ViewModels.Seller
{
    public class SellerInfoOnHouseViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
