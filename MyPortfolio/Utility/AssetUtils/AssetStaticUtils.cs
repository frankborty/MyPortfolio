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

        internal static AssetCategory? GetTypeFromName(string assetTypeName, List<AssetCategory> assetCategoryList)
        {
            foreach (AssetCategory assetCategory in assetCategoryList)
            {
                if (string.Equals(assetTypeName, assetCategory.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return assetCategory;
                }
            }
            return null;
        }


        internal static Asset? GetAssetFromName(string assetName, List<Asset> assetList)
        {
            foreach (Asset asset in assetList)
            {
                if (string.Equals(assetName, asset.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return asset;
                }
            }
            return null;
        }

        internal static async Task<List<Asset>> ProcessAssetFile(IFormFile file, List<AssetCategory> assetCategoryList)
        {
            List<Asset> assetList = new List<Asset>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                Asset asset = ConvertFileLineInAsset(content, assetCategoryList);
                assetList.Add(asset);
            }
            return assetList;
        }

        private static Asset ConvertFileLineInAsset(string content, List<AssetCategory> assetCategoryList)
        {
            Asset asset = new Asset();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 4)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }
            asset.Name = lineTokens[0];
            if (decimal.TryParse(lineTokens[1].Replace(".", ","), out decimal number))
            {
                asset.Balance = number;
            }
            else
            {
                throw new Exception($"Invalid Amount: {lineTokens}");
            }
            asset.TimeStamp = GenericUtils.ConvertStringToDateTime(lineTokens[2]);
            AssetCategory? assetCategory = GetCategoryFromName(lineTokens[3], assetCategoryList);
            if (assetCategory is null)
            {
                throw new Exception($"Invalid asset type {lineTokens}");
            }

            asset.AssetCategory = assetCategory;
            return asset;
        }

        internal static async Task<List<Asset>> ProcessFinancialAssetFile(IFormFile file, List<AssetCategory> assetCategoryList)
        {
            List<Asset> assetList = new List<Asset>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                Asset asset = ConvertFileLineInFinancialAsset(content, assetCategoryList);
                assetList.Add(asset);
            }
            return assetList;
        }
        
        private static Asset ConvertFileLineInFinancialAsset(string content, List<AssetCategory> assetCategoryList)
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

            AssetCategory? assetCategory = GetTypeFromName(lineTokens[5], assetCategoryList);
            if (assetCategory is null)
            {
                throw new Exception($"Invalid asset type {lineTokens}");
            }

            asset.AssetCategory = assetCategory;
            return asset;
        }
    }
}
