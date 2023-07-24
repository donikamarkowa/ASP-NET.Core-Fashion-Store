using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionStoreSystem.Data.Models
{
    /// <summary>
    /// This is custom user class that works with the default ASP.NET Core Identity.
    /// You can add additional info to the built-in users.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [InverseProperty(nameof(Purchase.User))]
        public virtual ICollection<Purchase> Products { get; set; } = new List<Purchase>();

        [InverseProperty(nameof(Favorite.User))]
        public virtual ICollection<Favorite> FavoriteProducts { get; set; } = new List<Favorite>();
    }
}
