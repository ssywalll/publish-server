using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank_Number",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Orders",
                newName: "BankAccount_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Meal_Date",
                table: "Orders",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BankAccounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Bank_Number",
                table: "BankAccounts",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Food_Drink_Id",
                table: "Tags",
                column: "Food_Drink_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Food_Drink_Id",
                table: "Reviews",
                column: "Food_Drink_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_User_Id",
                table: "Reviews",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BankAccount_Id",
                table: "Orders",
                column: "BankAccount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User_Id",
                table: "Orders",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodDrinkOrders_Food_Drink_Id",
                table: "FoodDrinkOrders",
                column: "Food_Drink_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodDrinkOrders_Order_Number",
                table: "FoodDrinkOrders",
                column: "Order_Number");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Food_Drink_Id",
                table: "Carts",
                column: "Food_Drink_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_User_Id",
                table: "Carts",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_User_Id",
                table: "BankAccounts",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "Fk_BankAccount_User",
                table: "BankAccounts",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Cart_FoodDrinkMenu",
                table: "Carts",
                column: "Food_Drink_Id",
                principalTable: "FoodDrinkMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Cart_User",
                table: "Carts",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_FoodDrinkOrder_FoodDrinkMenu",
                table: "FoodDrinkOrders",
                column: "Food_Drink_Id",
                principalTable: "FoodDrinkMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_FoodDrinkOrder_Order",
                table: "FoodDrinkOrders",
                column: "Order_Number",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Order_BankAccount",
                table: "Orders",
                column: "BankAccount_Id",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Order_User",
                table: "Orders",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Review_FoodDrinkMenu",
                table: "Reviews",
                column: "Food_Drink_Id",
                principalTable: "FoodDrinkMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Review_User",
                table: "Reviews",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "Fk_Tag_FoodDrinkMenu",
                table: "Tags",
                column: "Food_Drink_Id",
                principalTable: "FoodDrinkMenus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_BankAccount_User",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "Fk_Cart_FoodDrinkMenu",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "Fk_Cart_User",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "Fk_FoodDrinkOrder_FoodDrinkMenu",
                table: "FoodDrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "Fk_FoodDrinkOrder_Order",
                table: "FoodDrinkOrders");

            migrationBuilder.DropForeignKey(
                name: "Fk_Order_BankAccount",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "Fk_Order_User",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "Fk_Review_FoodDrinkMenu",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "Fk_Review_User",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "Fk_Tag_FoodDrinkMenu",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Food_Drink_Id",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_Food_Drink_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_User_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BankAccount_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_User_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_FoodDrinkOrders_Food_Drink_Id",
                table: "FoodDrinkOrders");

            migrationBuilder.DropIndex(
                name: "IX_FoodDrinkOrders_Order_Number",
                table: "FoodDrinkOrders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Food_Drink_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_User_Id",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_User_Id",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Bank_Number",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "BankAccount_Id",
                table: "Orders",
                newName: "Number");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Meal_Date",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "Bank_Number",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BankAccounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "BankAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
