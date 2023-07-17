using System.ComponentModel.DataAnnotations;

namespace FashionStoreSystem.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Color { get; set; } = null!;

        [Required]
        public string Size { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}