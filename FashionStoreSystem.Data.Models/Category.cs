using System.ComponentModel.DataAnnotations;

using static FashionStoreSystem.Common.EntityValidationConstants.Category;
namespace FashionStoreSystem.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();   
    }
}
