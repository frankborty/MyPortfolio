using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetTypeRepo
    {
        Task<IEnumerable<AssetType>> GetAllAssetTypeAsync();
        Task<AssetType?> GetAssetTypeByIdAsync(int id);
        Task<AssetType?> GetAssetTypeByNameAsync(string name);
        Task<AssetType?> AddAssetTypeAsync(AssetType entity);
        Task AddAssetTypeListAsync(List<AssetType> assetTypeList);        
        Task<AssetType> UpdateAssetTypeAsync(int id, AssetType entity);
        Task DeleteAssetTypeAsync(int id);
    }
}
