using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        public virtual ICollection<Purchase> Products { get; set; } = new List<Purchase>();

        public virtual ICollection<Favorite> FavoriteProducts { get; set; } = new List<Favorite>();
    }
}
