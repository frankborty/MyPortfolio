using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Models.Assets;

namespace MyPortfolio.Utility.AssetUtils
{
    public class AssetStaticUtils
    {
        public static async Task AddSingleAsset(IAssetRepo _assetRepo, Asset assetToAdd)
        {
            await _assetRepo.AddAssetAsync(assetToAdd);
        }

        internal static AssetCategory? GetCategoryFromName(string assetCategoryName, List<AssetCategory> assetCategoryList)
        {
            foreach (AssetCategory assetCategory in assetCategoryList)
            {
                if (string.Equals(assetCategoryName, assetCategory.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assetCategory;
                }
            }
            return null;
        }

        internal static AssetType? GetTypeFromName(string assetTypeName, List<AssetType> assetTypeList)
        {
            foreach (AssetType assetType in assetTypeList)
            {
                if (string.Equals(assetTypeName, assetType.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assetType;
                }
            }
            return null;
        }

        internal static async Task<List<Asset>> ProcessAssetFile(IFormFile file, List<AssetType> assetTypeList)
        {
            List<Asset> assetList = new List<Asset>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                Asset asset = ConvertFileLineInAsset(content, assetTypeList);
                assetList.Add(asset);
            }
            return assetList;
        }

        private static Asset ConvertFileLineInAsset(string content, List<AssetType> assetTypeList)
        {
            Asset asset = new Asset();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 2)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }
            if (decimal.TryParse(lineTokens[1].Replace(".", ","), out decimal number))
            {
                asset.Balance = number;
            }
            else
            {
                throw new Exception($"Invalid Amount: {lineTokens}");
            }

            AssetType? assetType = GetTypeFromName(lineTokens[0], assetTypeList);
            if (assetType is null)
            {
                throw new Exception($"Invalid asset type {lineTokens}");
            }

            asset.AssetType = assetType;
            return asset;
        }

        internal static async Task<List<Asset>> ProcessFinancialAssetFile(IFormFile file, List<AssetType> assetTypeList)
        {
            List<Asset> assetList = new List<Asset>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                Asset asset = ConvertFileLineInFinancialAsset(content, assetTypeList);
                assetList.Add(asset);
            }
            return assetList;
        }

        private static Asset ConvertFileLineInFinancialAsset(string content, List<AssetType> assetTypeList)
        {
            Asset asset = new Asset();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 6)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }

            asset.ISIN = lineTokens[0];
            asset.Name = lineTokens[1];
            if (decimal.TryParse(lineTokens[2].Replace(".", ","), out decimal balance))
            {
                asset.Balance = balance;
            }
            else
            {
                throw new Exception($"Invalid Balance: {lineTokens}");
            }
            if (int.TryParse(lineTokens[3].Replace(".", ","), out int share))
            {
                asset.Share = share;
            }
            else
            {
                throw new Exception($"Invalid Share: {lineTokens}");
            }
            if (decimal.TryParse(lineTokens[4].Replace(".", ","), out decimal avgPrice))
            {
                asset.AvgPrice = avgPrice;
            }
            else
            {
                throw new Exception($"Invalid AvgPrice: {lineTokens}");
            }

            AssetType? assetType = GetTypeFromName(lineTokens[5], assetTypeList);
            if (assetType is null)
            {
                throw new Exception($"Invalid asset type {lineTokens}");
            }

            asset.AssetType = assetType;
            return asset;
        }
    }
}
