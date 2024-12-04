using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models.Expenses
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Collection<ExpenseType>? ExpenseTypes { get; set; }
    }
}
