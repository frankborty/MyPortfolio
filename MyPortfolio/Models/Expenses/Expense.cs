using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Expenses
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }= string.Empty;
        public int TypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
