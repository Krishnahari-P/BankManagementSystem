using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
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
                values: new object[] { "854180de-93ef-475c-962d-c95d86efc73b", "AQAAAAIAAYagAAAAEHlMudkgE8u5PzSLpiOawkRNnTyymsFN6aADk+K1+UFRArk5luywHZUrk7yJ/7g1MA==", "a1bd5c2f-88a8-4381-8bcb-d66ac0e0081e" });

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
                values: new object[] { "3c225a71-2b98-474a-902b-5de8b2e29f06", "AQAAAAIAAYagAAAAEMKMq3VKO9d6v6e10Vo8YgfQ5txDJHQvSEdJEHZnEnXpHHQHv89p0jdT8SH9cnzeWg==", "27607927-1f4c-44d9-b0bf-04a53d662a73" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Account",
                column: "AccountNumber",
                unique: true);
        }
    }
}
