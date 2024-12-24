namespace MyPortfolio.Models.Assets
{
    public class AssetValue
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public Asset? Asset { get; set; }
    }
}
