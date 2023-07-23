using FashionStoreSystem.Web.ViewModels.Favorite;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IFavoriteService
    {
        Task AddToFavoriteAsync(string productId, string userId);

        Task RemoveFromFavoriteAsync(string productId, string userId);

        Task<IEnumerable<FavoriteViewModel>> GetFavoriteByUserIdAsync(string userId);

        Task<bool> IsProductInFavoriteByUserIdAsync(string productId, string userId);
    }
}
