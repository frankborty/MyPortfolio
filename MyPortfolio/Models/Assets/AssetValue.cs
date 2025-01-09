namespace MyPortfolio.Models.Assets
{
    public class AssetValue
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public Asset Asset { get; set; } = new Asset();
    }
}
