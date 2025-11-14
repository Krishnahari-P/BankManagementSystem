using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AccountNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_AccountNumber",
                table: "Account");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bdbc4d7-b76b-40ef-b1b0-91b8027631ab", "AQAAAAIAAYagAAAAEAHw/oK6WY6V0aGcq4U326BiOBYpcwenaG+IjOQ0F3eK6MSuNemGeAW7p38H/79Iiw==", "df1b969d-a38c-4a30-ad4b-382332d39c0c" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Account",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_AccountNumber",
                table: "Account");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0e98765-b2bc-4580-8289-41ad2201d2be", "AQAAAAIAAYagAAAAEJU/ko9dxQMD+0MyqC1bUgyCP+IrbxyedBWcAGNr0irOLxL72j4k9exiODTWYDdjnw==", "dfaa6012-215d-44de-9fab-9cc3bdba5a9b" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Account",
                column: "AccountNumber",
                unique: true);
        }
    }
}
