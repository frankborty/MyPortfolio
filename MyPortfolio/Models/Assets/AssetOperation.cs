namespace MyPortfolio.Models.Assets
{
    public class AssetOperation
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Share { get; set; }
        public decimal AvgPrice { get; set; }
        public DateTime Date { get; set; }
        public string OperationType { get; set; } = "BUY"; //BUY, SELL
        public Asset? Asset { get; set; }
    }
}
