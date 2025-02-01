using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using System.Globalization;

namespace MyPortfolio.Utility.IncomeUtils
{
    public class IncomeStaticUtils
    {
        public static async Task AddSingleIncome(IIncomeRepo _incomeRepo, Income incomeToAdd)
        {
            await _incomeRepo.AddIncomeAsync(incomeToAdd);
        }

        public static async Task<List<Income>> ProcessIncomeFile(IFormFile file, List<IncomeType> incomeTypeList)
        {
            List<Income> incomeList = new List<Income>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                incomeList.Add(ConvertFileLineInIncomeList(content, incomeTypeList));
            }
            return incomeList;
        }

        private static Income ConvertFileLineInIncomeList(string content, List<IncomeType> incomeTypeList)
        {
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 4)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }

            IncomeType? incomeType = GetTypeFromName(lineTokens[0], incomeTypeList);
            if (incomeType is null)
            {
                throw new Exception($"Invalid expense type {lineTokens}");
            }

            Income income = new Income();
            income.IncomeType = incomeType;
            try
            {
                income.Date = DateTime.ParseExact(lineTokens[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (decimal.TryParse(lineTokens[2], out decimal number))
            {
                income.Amount = number;
            }
            else
            {
                throw new Exception($"Invalid Amount: {lineTokens}");
            }

            if (lineTokens[3] != ";")
            {
                income.Note = lineTokens[3];
            }


            return income;
        }

        private static IncomeType? GetTypeFromName(string incomeTypeName, List<IncomeType> incomeTypeList)
        {
            foreach (IncomeType income in incomeTypeList)
            {
                if (string.Equals(incomeTypeName, income.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return income;
                }
            }
            return null;
        }
    }
}
