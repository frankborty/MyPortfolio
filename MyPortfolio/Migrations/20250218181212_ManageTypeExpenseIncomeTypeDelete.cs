using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.Migrations
{
    /// <inheritdoc />
    public partial class ManageTypeExpenseIncomeTypeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseTypes_TypeId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseTypes_TypeId",
                table: "Expenses",
                column: "TypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseTypes_TypeId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseTypes_TypeId",
                table: "Expenses",
                column: "TypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
