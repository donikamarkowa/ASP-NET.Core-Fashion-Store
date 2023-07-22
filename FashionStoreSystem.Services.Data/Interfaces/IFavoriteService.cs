namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IFavoriteService
    {
        Task AddToFavoriteAsync(string productId, string userId);
        Task RemoveFromFavoriteAsync(string productId, string userId);
    }
}
