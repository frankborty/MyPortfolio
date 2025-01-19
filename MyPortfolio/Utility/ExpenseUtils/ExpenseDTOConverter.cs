using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;

namespace MyPortfolio.Utility.ExpenseUtils
{
    public static class ExpenseDTOConverter
    {
        public static ExpenseDTO ToExpenseDTO(Expense expense)
        {
            return new ExpenseDTO()
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = GenericUtils.ConvertDateTimeToString(expense.Date),
                Note = expense.Note,
                ExpenseType = ExpenseTypeDTOConverter.ToExpenseTypeDTO(expense.ExpenseType)
            };
        }

        public static Expense FromExpenseToAddDTO(ExpenseToAddDTO expenseToAdd, ExpenseType? expenseType)
        {
            if(expenseType is null)
            {
                throw new Exception("Invalid type");
            }
            return new Expense
            {
                Amount = expenseToAdd.Amount,
                Description = expenseToAdd.Description,
                Date = GenericUtils.ConvertStringToDateTime(expenseToAdd.Date),
                Note = expenseToAdd.Note,
                TypeId = expenseType.Id
            };
        }
    }

    public static class ExpenseTypeDTOConverter
    {
        public static ExpenseTypeDTO ToExpenseTypeDTO(ExpenseType? expenseType)
        {
            if (expenseType is null)
            {
                return new ExpenseTypeDTO();
            }
            return new ExpenseTypeDTO()
            {
                Id = expenseType.Id,
                Name = expenseType.Name,
                Category = ExpenseCategoryDTOConverter.ToExpenseCategoryDTO(expenseType.Category)
            };
        }

        public static ExpenseType FromExpenseTypeToAddDTO(ExpenseTypeToAddDTO expenseTypeToAdd)
        {
            return new ExpenseType()
            {
                Name = expenseTypeToAdd.Name,
                CategoryId = expenseTypeToAdd.CategoryId
            };
        }
    }

    public static class ExpenseCategoryDTOConverter
    {
        public static ExpenseCategoryDTO ToExpenseCategoryDTO(ExpenseCategory? expenseCategory)
        {
            if (expenseCategory is null)
            {
                return new ExpenseCategoryDTO();
            }
            return new ExpenseCategoryDTO()
            {
                Id = expenseCategory.Id,
                Name = expenseCategory.Name
            };
        }


        public static ExpenseCategoryAndTypesDTO ToExpenseCategoryAndTypeDTO(ExpenseCategory? expenseCategory)
        {
            if (expenseCategory is null)
            {
                return new ExpenseCategoryAndTypesDTO();
            }
            return new ExpenseCategoryAndTypesDTO()
            {
                Id = expenseCategory.Id,
                Name = expenseCategory.Name
            };
        }
    }
}
