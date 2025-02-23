using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.DTO.AssetDTO;
using MyPortfolio.Models.Assets;
using System.Collections.Immutable;
using System.Globalization;

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

        private static Asset ConvertFileLineInAsset(string content, List<AssetCategory> assetCategoryList)
        {
            Asset asset = new Asset();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 4)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }
            asset.Name = lineTokens[0];
            AssetCategory? assetCategory = GetCategoryFromName(lineTokens[3], assetCategoryList);
            if (assetCategory is null)
            {
                throw new Exception($"Invalid asset type {lineTokens}");
            }

            asset.AssetCategory = assetCategory;
            return asset;
        }

        internal static async Task<List<AssetValue>> ProcessAssetValueFile(IFormFile file, List<Asset> assetList)
        {
            List<AssetValue> assetValueList = new List<AssetValue>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                if (content.Length > 3)
                {
                    AssetValue assetValue = ConvertFileLineInAssetValue(content, assetList);
                    assetValueList.Add(assetValue);
                }
            }
            return assetValueList;
        }

        private static AssetValue ConvertFileLineInAssetValue(string content, List<Asset> assetList)
        {
            AssetValue assetValue = new AssetValue();
            string[] lineTokens = content.Split(' ');
            if (lineTokens.Length != 3)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }

            var asset = GetAssetFromName(lineTokens[0], assetList);
            if (asset is null)
            {
                throw new Exception($"Invalid Asset: {lineTokens}");
            }
            assetValue.Asset = asset;
            assetValue.AssetId = asset.Id;
            assetValue.TimeStamp = DateTime.ParseExact(lineTokens[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (decimal.TryParse(lineTokens[2].Replace(".", ","), out decimal value))
            {
                assetValue.Value = value;
            }
            else
            {
                throw new Exception($"Invalid Share: {lineTokens}");
            }

            return assetValue;
        }

        internal static async Task<List<AssetOperation>> ProcessAssetOperationFile(IFormFile file, List<Asset> assetList)
        {
            List<AssetOperation> assetOperationList = new List<AssetOperation>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                if (content.Length > 3)
                {
                    AssetOperation assetValue = ConvertFileLineInAssetOperation(content, assetList);
                    assetOperationList.Add(assetValue);
                }
            }
            return assetOperationList;
        }

        private static AssetOperation ConvertFileLineInAssetOperation(string content, List<Asset> assetList)
        {
            AssetOperation assetOperation = new AssetOperation();
            string[] lineTokens = content.Split(' ');
            if (lineTokens.Length != 4)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }

            var asset = GetAssetFromName(lineTokens[0], assetList);
            if (asset is null)
            {
                throw new Exception($"Invalid Asset: {lineTokens}");
            }
            assetOperation.AssetId = asset.Id;
            
            if (decimal.TryParse(lineTokens[1].Replace(".", ","), out decimal share))
            {
                assetOperation.Share = share;
            }
            else
            {
                throw new Exception($"Invalid Share: {lineTokens}");
            }
            
            if (decimal.TryParse(lineTokens[2].Replace(".", ","), out decimal price))
            {
                assetOperation.AvgPrice = price;
            }
            else
            {
                throw new Exception($"Invalid Price: {lineTokens}");
            }
            assetOperation.Date = DateTime.ParseExact(lineTokens[3], "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            assetOperation.OperationType = "BUY";


            return assetOperation;
        }

        internal static AssetValueListDTO CreateMonthValueList(IGrouping<Asset, AssetValue> assetValueList)
        {
            AssetValueListDTO assetValueResult = new AssetValueListDTO()
            {
                Asset = AssetDTOConverter.ToAssetSimpleDTO(assetValueList.Key)
            };

            var groupedByMonth = assetValueList.GroupBy(a => new { a.TimeStamp.Year, a.TimeStamp.Month }).ToList();

            foreach (var monthValueList in groupedByMonth)
            {
                var orderdMonthValue = monthValueList.OrderBy(g => g.TimeStamp).ToList();
                assetValueResult.AssetValueList.Add(new AssetValueDTO()
                {
                    Value = orderdMonthValue.Last().Value,
                    TimeStamp = GenericUtils.ConvertDateTimeToString(orderdMonthValue.Last().TimeStamp)
                });
            }

            //assetValueResult.AssetValueList = assetValueResult.AssetValueList.OrderBy(av => av.TimeStamp).ToList();

            return assetValueResult;
        }
    }
}
