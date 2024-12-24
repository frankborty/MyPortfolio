namespace MyPortfolio.Models.Assets
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public decimal AvgPrice { get; set; }
        public int TypeId { get; set; }
        public AssetType? AssetType { get; set; }
        public List<AssetOperation>? OperationList { get; set; }
        public List<AssetValue>? ValueList { get; set; }
    }
}
