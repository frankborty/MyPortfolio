using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility;
using MyPortfolio.Utility.AssetUtils;
using MyPortfolio.Utility.ExpenseUtils;
using Swashbuckle.AspNetCore.Annotations;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedController : ControllerBase
    {
        private readonly IExpenseTypeRepo _expenseTypeRepo;
        private readonly IExpenseCategoryRepo _expenseCategoryRepo;
        private readonly IAssetTypeRepo _assetTypeRepo;
        private readonly IAssetCategoryRepo _assetCategoryRepo;
        private readonly IIncomeTypeRepo _incomeTypeRepo;
        public DataSeedController(IExpenseTypeRepo expenseTypeRepo,
            IExpenseCategoryRepo expenseCategoryRepo,
            IAssetTypeRepo assetTypeRepo,
            IAssetCategoryRepo assetCategoryRepo,
            IIncomeTypeRepo incomeTypeRepo)
        {
            _expenseTypeRepo = expenseTypeRepo;
            _expenseCategoryRepo = expenseCategoryRepo;
            _assetTypeRepo = assetTypeRepo;
            _assetCategoryRepo = assetCategoryRepo;
            _incomeTypeRepo = incomeTypeRepo;
        }

        [HttpPost]
        [Route("fullSchema")]
        [SwaggerOperation(Summary = "Seed fulle schema")]
        public async Task<IActionResult> PostFullSchema()
        {
            try
            {
                await SeedUtils.SetExpenseSchema(_expenseTypeRepo, _expenseCategoryRepo);
                await SeedUtils.SetIncomeSchema(_incomeTypeRepo);
                await SeedUtils.SetAssetSchema(_assetTypeRepo, _assetCategoryRepo);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("expenseSchema")]
        [SwaggerOperation(Summary = "Seed expense schema")]
        public async Task<IActionResult> PostExpenseSchema()
        {
            try
            {
                await SeedUtils.SetExpenseSchema(_expenseTypeRepo, _expenseCategoryRepo);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("incomeSchema")]
        [SwaggerOperation(Summary = "Seed income schema")]
        public async Task<IActionResult> PostIncomeSchema()
        {
            try
            {
                await SeedUtils.SetIncomeSchema(_incomeTypeRepo);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("assetSchema")]
        [SwaggerOperation(Summary = "Seed asset schema")]
        public async Task<IActionResult> PostAssetSchema()
        {
            try
            {
                await SeedUtils.SetAssetSchema(_assetTypeRepo, _assetCategoryRepo);
                
                return Ok();
            }
            catch (Exception ex)
            {
                // Log dell'errore (es. con un logger, se configurato)
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
