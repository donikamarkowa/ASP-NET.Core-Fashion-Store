using System.ComponentModel.DataAnnotations.Schema;

namespace FashionStoreSystem.Data.Models
{
    public class Favorite
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
