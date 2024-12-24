using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Models.Assets;
using MyPortfolio.Models.Expenses;
using MyPortfolio.Models.Incomes;
using MyPortfolio.Utility.AssetUtils;
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
        private readonly IAssetRepo _assetRepo;
        private readonly IAssetTypeRepo _assetTypeRepo;

        public FileManagerController(IExpenseCategoryRepo expenseCategoryRepo, IExpenseRepo expenseRepo, IExpenseTypeRepo expenseTypeRepo,
            IIncomeRepo incomeRepo, IIncomeTypeRepo incomeTypeRepo,
            IAssetRepo assetRepo, IAssetTypeRepo assetTypeRepo)
        {
            _expenseCategoryRepo = expenseCategoryRepo;
            _expenseRepo = expenseRepo;
            _expenseTypeRepo = expenseTypeRepo;
            _incomeRepo = incomeRepo;
            _incomeTypeRepo = incomeTypeRepo;
            _assetRepo = assetRepo;
            _assetTypeRepo = assetTypeRepo;
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

        [HttpPost]
        [Route("asset")]
        [SwaggerOperation(Summary = "Add asset from file")]
        public async Task<IActionResult> AddAssetFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file fornito o file vuoto.");
            }
            try
            {
                var assetTypeCollection = await _assetTypeRepo.GetAllAssetTypeAsync();
                List<AssetType> assetTypeList = assetTypeCollection.ToList();
                List<Asset> assetList = await AssetStaticUtils.ProcessAssetFile(file, assetTypeList);
                await _assetRepo.AddAssetListAsync(assetList);
                return Ok($"{assetList.Count} asset added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante il caricamento del file: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("financialAsset")]
        [SwaggerOperation(Summary = "Add financial asset from file")]
        public async Task<IActionResult> AddFinancialAssetFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file fornito o file vuoto.");
            }
            try
            {
                var assetTypeCollection = await _assetTypeRepo.GetAllAssetTypeAsync();
                List<AssetType> assetTypeList = assetTypeCollection.ToList();
                List<Asset> assetList = await AssetStaticUtils.ProcessFinancialAssetFile(file, assetTypeList);
                await _assetRepo.AddAssetListAsync(assetList);
                return Ok($"{assetList.Count} asset added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante il caricamento del file: {ex.Message}");
            }
        }
    }
}
