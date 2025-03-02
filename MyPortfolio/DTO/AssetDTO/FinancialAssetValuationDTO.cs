namespace MyPortfolio.DTO.AssetDTO
{
    public class FinancialAssetValuationDTO
    {
        public AssetDTO Asset { get; set; } = new AssetDTO();
        public decimal InitialValue { get; set; } = 0;
        public decimal FinalValue { get; set; } = 0;
        public decimal AbsDelta { get; set; } = 0;
        public decimal PercentDelta { get; set; } = 0;
    }
}
