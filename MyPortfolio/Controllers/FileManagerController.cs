using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.DTO.ExpenseDTO;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Utility.ExpenseUtils;
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

        public FileManagerController(IExpenseCategoryRepo expenseCategoryRepo, IExpenseRepo expenseRepo, IExpenseTypeRepo expenseTypeRepo)
        {
            _expenseCategoryRepo = expenseCategoryRepo;
            _expenseRepo = expenseRepo;
            _expenseTypeRepo = expenseTypeRepo;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add expensive")]
        public async Task<IActionResult> AddExpenseFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file fornito o file vuoto.");
            }
            try
            {
                var expenseTypeCollection = await _expenseTypeRepo.GetAllExpenseTypesAsync();
                List<ExpenseType> expenseTypeList = expenseTypeCollection.ToList();
                List<Expense> expenseList = await ExpenseStaticUtils.ProcessExpenseFile(file, expenseTypeList);
                await _expenseRepo.AddExpenseListAsync(expenseList);
                return Ok($"{expenseList.Count} expense added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante il caricamento del file: {ex.Message}");
            }
        }
    }
}
