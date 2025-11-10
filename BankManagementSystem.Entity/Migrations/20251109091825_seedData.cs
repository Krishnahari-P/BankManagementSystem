using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Entity.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "46b36bf0-15ee-4631-aa11-8a7007aca77c", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2b9d215a-65b4-44b7-872e-a5780fb66fd6", 0, "13fd639e-0304-4334-8eff-da3f920af146", "admin@email.com", false, true, false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEHzJDsaDaBf01jw6RHkv63N8CHqK4fvXoQj4ECvSAjlRDPQv34TDshGAwGOeeAmeUw==", null, false, "c99d1d82-8b37-455a-af59-e3ed22c3baf4", "Approved", false, "admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "46b36bf0-15ee-4631-aa11-8a7007aca77c", "2b9d215a-65b4-44b7-872e-a5780fb66fd6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "46b36bf0-15ee-4631-aa11-8a7007aca77c", "2b9d215a-65b4-44b7-872e-a5780fb66fd6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46b36bf0-15ee-4631-aa11-8a7007aca77c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2b9d215a-65b4-44b7-872e-a5780fb66fd6");
        }
    }
}
