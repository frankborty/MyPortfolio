namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetValueDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Value { get; set; }
        public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        public AssetValueDTO()
        {
            Id = -1;
        }
    }
}
