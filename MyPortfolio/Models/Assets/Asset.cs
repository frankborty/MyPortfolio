namespace MyPortfolio.Models.Assets
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ISIN { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal Share { get; set; }
        public int PMC { get; set; } = 0;
        public int CategoryId { get; set; }
        public AssetCategory? Category { get; set; }
        public List<AssetOperation>? OperationList { get; set; }
        public List<AssetValue>? ValueList { get; set; }
    }
}
