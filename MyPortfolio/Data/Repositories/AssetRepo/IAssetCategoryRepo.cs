using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetCategoryRepo
    {
        Task<IEnumerable<AssetCategory>> GetAllAssetCategoryAsync();
        Task<AssetCategory?> GetAssetCategoryByIdAsync(int id);
        Task<AssetCategory?> AddAssetCategoryAsync(AssetCategory entity);
        Task AddAssetCategoryListAsync(List<AssetCategory> assetCategoryList);
        Task<AssetCategory> UpdateAssetCategoryAsync(int id, AssetCategory entity);
        Task DeleteAssetCategoryAsync(int id);
    }
}
