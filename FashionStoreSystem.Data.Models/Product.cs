using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FashionStoreSystem.Common.EntityValidationConstants.Product;

namespace FashionStoreSystem.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(SizeMaxLength)]
        public string Size { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Category))]  
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [ForeignKey(nameof(Seller))]
        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; } = null!;


        [InverseProperty(nameof(Purchase.Product))]
        public virtual ICollection<Purchase> Users { get; set; } = new List<Purchase>();

        [InverseProperty(nameof(Favorite.Product))]
        public virtual ICollection<Favorite> FavoriteProducts { get; set; } = new List<Favorite>();

        [InverseProperty(nameof(Cart.Product))]
        public virtual ICollection<Cart> CartProducts { get; set; } = new List<Cart>(); 
    }
}