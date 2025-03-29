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
            if (decimal.TryParse(lineTokens[2], out decimal value))
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
                assetOperation.PMC = price;
            }
            else
            {
                throw new Exception($"Invalid Price: {lineTokens}");
            }
            assetOperation.Date = DateTime.ParseExact(lineTokens[3], "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            assetOperation.OperationType = "BUY";


            return assetOperation;
        }

        internal static AssetValueListDTO CreateMonthValueList(IGrouping<Asset, AssetValue> assetValueList, IEnumerable<AssetOperation> assetOperationList)
        {
            AssetValueListDTO assetValueResult = new AssetValueListDTO()
            {
                Asset = AssetDTOConverter.ToAssetDTO(assetValueList.Key)
            };
            bool isFinancial = assetValueResult.Asset.Category.IsInvested;
            var groupedByMonth = assetValueList.GroupBy(a => new { a.TimeStamp.Year, a.TimeStamp.Month }).ToList();

            foreach (var monthValueList in groupedByMonth)
            {
                var orderdMonthValue = monthValueList.OrderBy(g => g.TimeStamp).ToList();

                decimal shareNumber = GetShareNumber(assetValueResult.Asset.Id, orderdMonthValue.Last().TimeStamp, assetOperationList);

                assetValueResult.AssetValueList.Add(new AssetValueDTO()
                {
                    Value = isFinancial ? orderdMonthValue.Last().Value * shareNumber : orderdMonthValue.Last().Value,
                    TimeStamp = orderdMonthValue.Last().TimeStamp,
                    Note = orderdMonthValue.Last().Note
                });
            }

            //assetValueResult.AssetValueList = assetValueResult.AssetValueList.OrderBy(av => av.TimeStamp).ToList();

            return assetValueResult;
        }



        internal static AssetValueListDTO CreateUnitPriceMonthValueList(IGrouping<Asset, AssetValue> assetValueList, IEnumerable<AssetOperation> assetOperationList)
        {
            AssetValueListDTO assetValueResult = new AssetValueListDTO()
            {
                Asset = AssetDTOConverter.ToAssetDTO(assetValueList.Key)
            };
            var groupedByMonth = assetValueList.GroupBy(a => new { a.TimeStamp.Year, a.TimeStamp.Month }).ToList();

            foreach (var monthValueList in groupedByMonth)
            {
                var orderdMonthValue = monthValueList.OrderBy(g => g.TimeStamp).ToList();
                assetValueResult.AssetValueList.Add(new AssetValueDTO()
                {
                    Value = orderdMonthValue.Last().Value,
                    TimeStamp = orderdMonthValue.Last().TimeStamp,
                    Note = orderdMonthValue.Last().Note
                });
            }

            //assetValueResult.AssetValueList = assetValueResult.AssetValueList.OrderBy(av => av.TimeStamp).ToList();

            return assetValueResult;
        }

        public static decimal GetShareNumber(int id, DateTime timeStamp, IEnumerable<AssetOperation> assetOperationList)
        {
            decimal result = 0;
            foreach (var assetOperation in assetOperationList) {
                if (assetOperation.AssetId == id)
                {
                    if (assetOperation.Date <= timeStamp)
                    {
                        result += assetOperation.Share;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return result;
        }
    }
}
