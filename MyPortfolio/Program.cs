using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Data.Repositories.AssetRepo;
using MyPortfolio.Data.Repositories.ExpenseRepo;
using MyPortfolio.Data.Repositories.IncomeRepo;
using MyPortfolio.Utility;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("it-IT");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("it-IT");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataDBConnection")));

builder.Services.AddScoped<IExpenseRepo, ExpenseRepo>();
builder.Services.AddScoped<IExpenseTypeRepo, ExpenseTypeRepo>();
builder.Services.AddScoped<IExpenseCategoryRepo, ExpenseCategoryRepo>();

builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IIncomeTypeRepo, IncomeTypeRepo>();

builder.Services.AddScoped<IAssetRepo, AssetRepo>();
builder.Services.AddScoped<IAssetCategoryRepo, AssetCategoryRepo>();
builder.Services.AddScoped<IAssetOperationRepo, AssetOperationRepo>();
builder.Services.AddScoped<IAssetValueRepo, AssetValueRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});


builder.Services.AddCors(options =>
{
    ServiceFactory.ConfigureCors(builder.Configuration, options);
});


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

var application = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataDbContext>();

var pendingMigrations = await application.Database.GetPendingMigrationsAsync();
if (pendingMigrations != null)
    await application.Database.MigrateAsync();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
