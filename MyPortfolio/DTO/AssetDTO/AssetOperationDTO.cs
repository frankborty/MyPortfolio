namespace MyPortfolio.DTO.AssetDTO
{
    public class AssetOperationDTO
    {
        public int AssetOperationId { get; set; }
        public int AssetId { get; set; }
        public decimal Share { get; set; }
        public decimal PMC { get; set; }
        public DateTime Date { get; set; }
        public string OperationType { get; set; } = "BUY"; //BUY, SELL
    }

    public class AssetWithOperationListDTO
    {
        public AssetDTO Asset { get; set; } = new AssetDTO();
        public List<AssetOperationDTO> OperationList { get; set; } = new List<AssetOperationDTO>();
    }
}
