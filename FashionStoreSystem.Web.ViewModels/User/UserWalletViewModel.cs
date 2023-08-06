using System.ComponentModel.DataAnnotations;
using static FashionStoreSystem.Common.EntityValidationConstants.User;

namespace FashionStoreSystem.Web.ViewModels.User
{
    public class UserWalletViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "My Money")]

        [Range(typeof(decimal), WalletMinLength, WalletMaxLength)]
        public decimal Wallet { get; set; }
    }
}
