﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FashionStoreSystem.Common.EntityValidationConstants.User;

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

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public decimal Wallet { get; set; }

        [InverseProperty(nameof(Purchase.User))]
        public virtual ICollection<Purchase> Products { get; set; } = new List<Purchase>();

        [InverseProperty(nameof(Favorite.User))]
        public virtual ICollection<Favorite> FavoriteProducts { get; set; } = new List<Favorite>();

    }
}
