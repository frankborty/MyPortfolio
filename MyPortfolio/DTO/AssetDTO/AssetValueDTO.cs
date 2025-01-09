namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetValueDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public AssetValueDTO()
        {
            Id = -1;
        }
    }
}
