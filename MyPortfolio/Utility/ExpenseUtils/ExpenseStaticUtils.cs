using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;
using System.Globalization;

namespace MyPortfolio.Utility.ExpenseUtils
{
    public class ExpenseStaticUtils
    {
        public static Expense CreateExpenseFromExpenseToAddDto(ExpenseToAddDTO expenseToAdd)
        {
            return new Expense()
            {
                Amount = expenseToAdd.Amount,
                Description = expenseToAdd.Description,
                Date = expenseToAdd.Date,
                Note = expenseToAdd.Note,
                TypeId = expenseToAdd.ExpenseTypeId
            };
        }

        public static ExpenseType CreateExpenseTypeFromExpenseTypeToAddDto(ExpenseTypeToAddDTO expenseTypeToAdd)
        {
            return new ExpenseType()
            {
                Name = expenseTypeToAdd.Name,
                CategoryId = expenseTypeToAdd.CategoryId
            };
        }

        public static async Task AddSingleExpense(IExpenseRepo _expenseRepo, Expense expenseToAdd)
        {
            await _expenseRepo.AddExpenseAsync(expenseToAdd);
        }

        public static async Task<List<Expense>> ProcessExpenseFile(IFormFile file, int year, List<ExpenseType> expenseTypeList)
        {
            List<Expense> expenseList = new List<Expense>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                Expense expense = ConvertFileLineInExpense(content, year, expenseTypeList);
                expenseList.Add(expense);
            }
            return expenseList;
        }

        private static Expense ConvertFileLineInExpense(string content, int year, List<ExpenseType> expenseTypeList)
        {
            Expense expense = new Expense();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 6)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }
            expense.Description = lineTokens[0];
            expense.Date = DateTime.ParseExact(lineTokens[1]+"/"+year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (decimal.TryParse(lineTokens[2].Replace(".", ","), out decimal number))
            {
                expense.Amount = number;
            }
            else
            {
                throw new Exception($"Invalid Amount: {lineTokens}");
            }

            ExpenseType? expenseType = ExtractExpenseTypeFromFileLine(lineTokens[5], expenseTypeList);
            if(expenseType is null)
            {
                throw new Exception($"Invalid expense type {lineTokens}");
            }

            expense.ExpenseType = expenseType;

            return expense;
        }

        private static ExpenseType? ExtractExpenseTypeFromFileLine(string expenseTypeName, List<ExpenseType> expenseTypeList)
        {
            foreach(ExpenseType expense in expenseTypeList)
            {
                if(string.Equals(expenseTypeName, expense.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return expense;
                }
            }
            return null;
        }
    }
}
