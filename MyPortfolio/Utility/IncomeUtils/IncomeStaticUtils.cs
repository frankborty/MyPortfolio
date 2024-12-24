using MyPortfolio.Data.Repositories.IncomeRepo;
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

        public static async Task<List<Income>> ProcessIncomeFile(IFormFile file, int year, List<IncomeType> incomeTypeList)
        {
            List<Income> incomeList = new List<Income>();
            List<string> fileContent = await FileManagerUtils.ReadIFileInStringList(file);
            foreach (string content in fileContent)
            {
                incomeList.AddRange(ConvertFileLineInIncomeList(content, year, incomeTypeList));
            }
            return incomeList;
        }

        private static List<Income> ConvertFileLineInIncomeList(string content, int year, List<IncomeType> incomeTypeList)
        {
            List<Income> incomeList = new List<Income>();
            string[] lineTokens = content.Split('\t');
            if (lineTokens.Length != 13)
            {
                throw new Exception($"Invalid Line: {lineTokens}");
            }

            IncomeType? incomeType = GetTypeFromName(lineTokens[0], incomeTypeList);
            if (incomeType is null)
            {
                throw new Exception($"Invalid expense type {lineTokens}");
            }

            for (int i = 1; i <= 12; i++)
            {
                Income income = new Income();
                income.IncomeType = incomeType;
                if (decimal.TryParse(lineTokens[i].Replace(".", ","), out decimal amount))
                {
                    income.Amount = amount;
                }
                else
                {
                    throw new Exception($"Invalid Amount: {lineTokens}");
                }
                income.Date = DateTime.ParseExact("01/" + i.ToString("D2") + "/" + year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                incomeList.Add(income);
            }
            return incomeList;
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
