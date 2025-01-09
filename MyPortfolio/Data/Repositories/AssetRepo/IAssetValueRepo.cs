using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetValueRepo
    {
        public Task<IEnumerable<AssetValue>> GetAllAssetValueAsync();
        public Task<AssetValue?> GetAssetValueByIdAsync(int id);
        public Task<IEnumerable<AssetValue>> GetAssetValueByAssetIdAsync(int assetId);
        public Task<IEnumerable<AssetValue>> GetAssetValueByAssetNameAsync(string assetName);
        public Task<AssetValue?> AddAssetValueAsync(AssetValue entity);
        public Task AddAssetValueListAsync(List<AssetValue> entityList);
        public Task<AssetValue> UpdateAssetValueAsync(int id, AssetValue entity);
        public Task DeleteAssetValueAsync(int id);
    }
}
