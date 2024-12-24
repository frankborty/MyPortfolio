using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetCategoryRepo
    {
        Task<IEnumerable<AssetCategory>> GetAllAssetCategoryAsync();
        Task<AssetCategory?> GetAssetCategoryByIdAsync(int id);
        Task<AssetCategory?> GetAssetCategoryByNameAsync(string name);
        Task<AssetCategory?> AddAssetCategoryAsync(AssetCategory entity);
        Task AddAssetCategoryListAsync(List<AssetCategory> assetCategoryList);
        Task<AssetCategory> UpdateAssetCategoryAsync(int id, AssetCategory entity);
        Task DeleteAssetCategoryAsync(int id);
    }
}
