using System.Collections.ObjectModel;

namespace MyPortfolio.Models.Expenses
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Collection<ExpenseType>? ExpenseTypes { get; set; }
    }
}
