using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Account : Migration
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
                values: new object[] { "f0e98765-b2bc-4580-8289-41ad2201d2be", "AQAAAAIAAYagAAAAEJU/ko9dxQMD+0MyqC1bUgyCP+IrbxyedBWcAGNr0irOLxL72j4k9exiODTWYDdjnw==", "dfaa6012-215d-44de-9fab-9cc3bdba5a9b" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Account",
                column: "AccountNumber",
                unique: true);
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
                values: new object[] { "114c01eb-06d4-41ff-b53e-c6ab101cfe3c", "AQAAAAIAAYagAAAAEHzIPOo3oVst74fWMoRgeXk2dDBO8gG1N62gzthU8stNYatX+DpVuOytv+QAydpYYg==", "97f6c803-1f50-4e56-9dd0-dfd831d42dd4" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Account",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL");
        }
    }
}
