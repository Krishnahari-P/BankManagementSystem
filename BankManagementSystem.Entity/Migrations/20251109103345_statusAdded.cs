using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class statusAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d25a01c-08ed-466d-a7e6-0119b2c5f0e9", "AQAAAAIAAYagAAAAEOp+y8KO95j8uebhKx5JUsXkjLEXU8pWdYJwC+vgipnDWbcLnwFOtD1nnMxsKCD6Hg==", "155e2dce-71b1-4cb6-bcc2-ad80ab74d347" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13fd639e-0304-4334-8eff-da3f920af146", "AQAAAAIAAYagAAAAEHzJDsaDaBf01jw6RHkv63N8CHqK4fvXoQj4ECvSAjlRDPQv34TDshGAwGOeeAmeUw==", "c99d1d82-8b37-455a-af59-e3ed22c3baf4" });
        }
    }
}
