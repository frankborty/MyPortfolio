using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Incomes
{
    public class IncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Collection<Income>? Incomes { get; set; }
    }
}
