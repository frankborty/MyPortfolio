using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetRepo
    {
        Task<IEnumerable<Asset>> GetAllAssetAsync();
        Task<Asset?> GetAssetByIdAsync(int id);
        Task<Asset?> AddAssetAsync(Asset entity);
        Task<Asset> UpdateAssetAsync(int id, Asset entity);
        Task DeleteAssetAsync(int id);
    }
}
