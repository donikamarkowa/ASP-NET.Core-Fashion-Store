using FashionStoreSystem.Services.Data.Models.Statistics;

namespace FashionStoreSystem.Services.Data.Interfaces
{
    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> GetStatisticsAsync();
    }
}
