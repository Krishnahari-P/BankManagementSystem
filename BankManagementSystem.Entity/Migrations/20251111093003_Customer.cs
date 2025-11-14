using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2f76f0c3-4bfa-4e60-aca6-82c3cd7d22bb", null, "Customer", "CUSTOMER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "114c01eb-06d4-41ff-b53e-c6ab101cfe3c", "AQAAAAIAAYagAAAAEHzIPOo3oVst74fWMoRgeXk2dDBO8gG1N62gzthU8stNYatX+DpVuOytv+QAydpYYg==", "97f6c803-1f50-4e56-9dd0-dfd831d42dd4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76f0c3-4bfa-4e60-aca6-82c3cd7d22bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "854180de-93ef-475c-962d-c95d86efc73b", "AQAAAAIAAYagAAAAEHlMudkgE8u5PzSLpiOawkRNnTyymsFN6aADk+K1+UFRArk5luywHZUrk7yJ/7g1MA==", "a1bd5c2f-88a8-4381-8bcb-d66ac0e0081e" });
        }
    }
}
