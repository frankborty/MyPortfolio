using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public class AssetValueRepo : IAssetValueRepo
    {
        private readonly DataDbContext dataDbContext;

        public AssetValueRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }
        public async Task<AssetValue?> AddAssetValueAsync(AssetValue entity)
        {
            var result = await dataDbContext.AssetValues.AddAsync(entity);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddAssetValueListAsync(List<AssetValue> entityList)
        {
            await dataDbContext.AssetValues.AddRangeAsync(entityList);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task DeleteAssetValueAsync(int id)
        {
            var asset = await dataDbContext.AssetValues.FindAsync(id);
            if (asset is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.AssetValues.Remove(asset);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AssetValue>> GetAllAssetValueAsync()
        {
            return await dataDbContext.AssetValues.ToListAsync();
        }

        public async Task<IEnumerable<IGrouping<Asset, AssetValue>>> GetAllAssetValueWithDetailsGroupByAssetIdAsync()
        {
            return await dataDbContext.AssetValues
                .Include(a => a.Asset)
                .ThenInclude(a => a.AssetCategory)
                .GroupBy(a => a.Asset)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetValue>> GetAllAssetValueWithDetailsAsync()
        {
            return await dataDbContext.AssetValues
                .Include(a => a.Asset)
                .ThenInclude(a => a.AssetCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<AssetValue>> GetAssetValueByAssetIdAsync(int assetId)
        {
            return await dataDbContext.AssetValues.Where(a => a.AssetId == assetId).ToListAsync();
        }

        public async Task<IEnumerable<AssetValue>> GetAssetValueByAssetNameAsync(string assetName)
        {
            return await dataDbContext.AssetValues.Where(a => a.Asset.Name == assetName).ToListAsync();
        }

        public async Task<AssetValue?> GetAssetValueByIdAsync(int id)
        {
            return await dataDbContext.AssetValues.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AssetValue> UpdateAssetValueAsync(int id, AssetValue entity)
        {
            AssetValue? assetValue = await dataDbContext.AssetValues
                .FirstOrDefaultAsync(e => e.Id == id);
            if (assetValue is null)
            {
                throw new KeyNotFoundException();
            }
            assetValue.AssetId = entity.AssetId;
            assetValue.TimeStamp = entity.TimeStamp;
            assetValue.Value = entity.Value;
            await dataDbContext.SaveChangesAsync();
            return assetValue;
        }
    }
}
