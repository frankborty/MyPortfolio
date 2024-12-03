using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.Expenses
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public ExpenseCategory? Category { get; set; }
        public Collection<Expense>? Expenses { get; set; }
    }
}
