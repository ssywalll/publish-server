using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "BankAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Bank_Number",
                table: "BankAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank_Number",
                table: "BankAccounts");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "BankAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
