using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.EntityValidationConstants.Seller;

namespace FashionStoreSystem.Data.Models
{
    public class Seller
    {
        public Seller()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
