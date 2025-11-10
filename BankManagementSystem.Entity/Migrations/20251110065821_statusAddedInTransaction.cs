using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class statusAddedInTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c225a71-2b98-474a-902b-5de8b2e29f06", "AQAAAAIAAYagAAAAEMKMq3VKO9d6v6e10Vo8YgfQ5txDJHQvSEdJEHZnEnXpHHQHv89p0jdT8SH9cnzeWg==", "27607927-1f4c-44d9-b0bf-04a53d662a73" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44926757-94fa-43d7-a6d7-6af4a5aa3f3f", "AQAAAAIAAYagAAAAEJoCQ49vJnTiGBEWSN5aim5XIBSoweEMrPUOBn47clq4Q5GNCYeIgSuDcjBp8WtbbQ==", "cb4f5e32-9299-413b-823a-ba915e3ede26" });
        }
    }
}
