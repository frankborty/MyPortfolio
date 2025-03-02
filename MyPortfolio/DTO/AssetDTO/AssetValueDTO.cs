namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetValueDTO
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Value { get; set; }
        public string TimeStamp { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string Note { get; set; } = string.Empty;
        public AssetValueDTO()
        {
            Id = -1;
        }
    }

    public class AssetValueListDTO
    {
        public AssetDTO Asset { get; set; } = new AssetDTO();
        public List<AssetValueDTO> AssetValueList { get; set; } = new List<AssetValueDTO>();
    }
}
