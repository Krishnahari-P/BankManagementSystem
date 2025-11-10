using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_ApplicationUserID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ApplicationUserID",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Employee",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovalDate",
                table: "Customer",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "CAST(GETDATE() AS DATE)");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Customer",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44926757-94fa-43d7-a6d7-6af4a5aa3f3f", "AQAAAAIAAYagAAAAEJoCQ49vJnTiGBEWSN5aim5XIBSoweEMrPUOBn47clq4Q5GNCYeIgSuDcjBp8WtbbQ==", "cb4f5e32-9299-413b-823a-ba915e3ede26" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ApplicationUserID",
                table: "Employee",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApplicationUserID",
                table: "Customer",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_ApplicationUserID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ApplicationUserID",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Employee",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApprovalDate",
                table: "Customer",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "CAST(GETDATE() AS DATE)",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Customer",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d25a01c-08ed-466d-a7e6-0119b2c5f0e9", "AQAAAAIAAYagAAAAEOp+y8KO95j8uebhKx5JUsXkjLEXU8pWdYJwC+vgipnDWbcLnwFOtD1nnMxsKCD6Hg==", "155e2dce-71b1-4cb6-bcc2-ad80ab74d347" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ApplicationUserID",
                table: "Employee",
                column: "ApplicationUserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ApplicationUserID",
                table: "Customer",
                column: "ApplicationUserID",
                unique: true);
        }
    }
}
