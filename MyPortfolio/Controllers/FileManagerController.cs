using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility.ExpenseUtils;
using MyPortfolio.Utility.IncomeUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        private readonly IExpenseRepo _expenseRepo;
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        private readonly IIncomeRepo _incomeRepo;
        private readonly IIncomeTypeRepo _incomeTypeRepo;

        public FileManagerController(IExpenseCategoryRepo expenseCategoryRepo, IExpenseRepo expenseRepo, IExpenseTypeRepo expenseTypeRepo,
            IIncomeRepo incomeRepo, IIncomeTypeRepo incomeTypeRepo)
        {
            _expenseCategoryRepo = expenseCategoryRepo;
            _expenseRepo = expenseRepo;
            _expenseTypeRepo = expenseTypeRepo;
            _incomeRepo = incomeRepo;
            _incomeTypeRepo = incomeTypeRepo;
        }

        [HttpPost]
        [Route("expense")]
        [SwaggerOperation(Summary = "Add expensive from file")]
        public async Task<IActionResult> AddExpenseFile(
            int year,
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file fornito o file vuoto.");
            }
            try
            {
                var expenseTypeCollection = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                List<ExpenseType> expenseTypeList = expenseTypeCollection.ToList();
                List<Expense> expenseList = await ExpenseStaticUtils.ProcessExpenseFile(file, year, expenseTypeList);
                await _expenseRepo.AddExpenseListAsync(expenseList);
                return Ok($"{expenseList.Count} expense added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante il caricamento del file: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("income")]
        [SwaggerOperation(Summary = "Add income from file")]
        public async Task<IActionResult> AddIncomeFile(
            int year,
            IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file fornito o file vuoto.");
            }
            try
            {
                var incomeTypeCollection = await _incomeTypeRepo.GetAllIncomeTypesAsync();
                List<IncomeType> incomeTypeList = incomeTypeCollection.ToList();
                List<Income> incomeList = await IncomeStaticUtils.ProcessIncomeFile(file, year, incomeTypeList);
                await _incomeRepo.AddIncomeListAsync(incomeList);
                return Ok($"{incomeList.Count} income added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante il caricamento del file: {ex.Message}");
            }
        }
    }
}
