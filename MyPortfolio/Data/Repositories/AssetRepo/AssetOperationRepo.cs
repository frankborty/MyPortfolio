using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public class AssetOperationRepo : IAssetOperationRepo
    {
        private readonly DataDbContext dataDbContext;

        public AssetOperationRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }
        public async Task<AssetOperation?> AddAssetOperationAsync(AssetOperation entity)
        {
            var result = await dataDbContext.AssetOperations.AddAsync(entity);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddAssetOperationListAsync(List<AssetOperation> entityList)
        {
            await dataDbContext.AssetOperations.AddRangeAsync(entityList);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task DeleteAssetOperationAsync(int id)
        {
            var asset = await dataDbContext.AssetOperations.FindAsync(id);
            if (asset is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.AssetOperations.Remove(asset);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetOperation>> GetAllAssetOperationAsync()
        {
            return await dataDbContext.AssetOperations.ToListAsync();
        }

        public async Task<IEnumerable<AssetOperation>> GetAllAssetOperationWithAssetAsync()
        {
            return await dataDbContext.AssetOperations
                .Include(o=>o.Asset)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetOperation>> GetAssetOperationByAssetIdAsync(int assetId)
        {
            return await dataDbContext.AssetOperations.Where(a => a.AssetId == assetId).ToListAsync();
        }

        public async Task<AssetOperation?> GetAssetOperationByIdAsync(int id)
        {
            return await dataDbContext.AssetOperations.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AssetOperation> UpdateAssetOperationAsync(int id, AssetOperation entity)
        {
            AssetOperation? assetOperation = await dataDbContext.AssetOperations
                .FirstOrDefaultAsync(e => e.Id == id);
            if (assetOperation is null)
            {
                throw new KeyNotFoundException();
            }
            assetOperation.AssetId = entity.AssetId;
            assetOperation.Date = entity.Date;
            assetOperation.OperationType = entity.OperationType;
            assetOperation.Share = entity.Share;
            await dataDbContext.SaveChangesAsync();
            return assetOperation;
        }
    }
}
