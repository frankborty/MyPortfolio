using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Expenses
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public ExpenseCategory? Category { get; set; }

        public Collection<Expense>? Expenses { get; set; }
    }
}
