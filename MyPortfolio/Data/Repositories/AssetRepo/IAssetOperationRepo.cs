using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public interface IAssetOperationRepo
    {
        public Task<IEnumerable<AssetOperation>> GetAllAssetOperationAsync();
        public Task<AssetOperation?> GetAssetOperationByIdAsync(int id);
        public Task<IEnumerable<AssetOperation>> GetAssetOperationByAssetIdAsync(int assetId);
        public Task<AssetOperation?> AddAssetOperationAsync(AssetOperation entity);
        public Task AddAssetOperationListAsync(List<AssetOperation> entityList);
        public Task<AssetOperation> UpdateAssetOperationAsync(int id, AssetOperation entity);
        public Task DeleteAssetOperationAsync(int id);
    }
}
