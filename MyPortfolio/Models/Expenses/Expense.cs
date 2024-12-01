using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Expenses
{
    public class Expense
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int TypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }
    }
}
