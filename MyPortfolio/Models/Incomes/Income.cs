using MyPortfolio.Models.Expenses;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.Incomes
{
    public class Income
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public int TypeId { get; set; }
        public IncomeType? IncomeType { get; set; }
    }
}
