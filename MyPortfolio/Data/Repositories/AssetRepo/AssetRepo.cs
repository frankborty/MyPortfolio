using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Data.Repositories.AssetRepo
{
    public class AssetRepo : IAssetRepo
    {
        private readonly DataDbContext dataDbContext;

        public AssetRepo(DataDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }

        public async Task<Asset?> AddAssetAsync(Asset asset)
        {
            var result = await dataDbContext.Assets.AddAsync(asset);
            await dataDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddAssetListAsync(List<Asset> entityList)
        {
            await dataDbContext.Assets.AddRangeAsync(entityList);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task DeleteAssetAsync(int assetId)
        {
            var asset = await dataDbContext.Assets.FindAsync(assetId);
            if (asset is null)
            {
                throw new KeyNotFoundException();
            }
            dataDbContext.Assets.Remove(asset);
            await dataDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asset>> GetAllAssetAsync()
        {
            return await dataDbContext.Assets
                .Include(a=>a.Category)
                .ToListAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(int assetId)
        {
            return await dataDbContext.Assets
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == assetId);
        }

        public async Task<Asset> UpdateAssetAsync(int assetId, Asset asset)
        {
            Asset? assetToUpdate = await dataDbContext.Assets
                .FirstOrDefaultAsync(e => e.Id == assetId);

            if (assetToUpdate is null)
            {
                throw new KeyNotFoundException();
            }

            assetToUpdate.Note = asset.Note;
            assetToUpdate.Share = asset.Share;
            assetToUpdate.CategoryId = asset.CategoryId;
            assetToUpdate.Url = asset.Url;
            assetToUpdate.ISIN = asset.ISIN;
            await dataDbContext.SaveChangesAsync();
            return assetToUpdate;
        }
    }
}
