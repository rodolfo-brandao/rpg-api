using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rpg.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDisabled", "Password", "PasswordSalt", "Role", "UpdatedAt", "Username" },
                values: new object[] { new Guid("9b003b60-6fd3-46bb-9d5f-a2527e780ec6"), new DateTime(2022, 6, 11, 21, 18, 51, 832, DateTimeKind.Utc).AddTicks(9644), "gordon.freeman@blackmesa.com", false, "83a6c7696be667964f0f42ac17f7fe93", "c0695027b298c139700d002f", 1, null, "freeman" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDisabled", "Password", "PasswordSalt", "Role", "UpdatedAt", "Username" },
                values: new object[] { new Guid("e95d6bdb-42f8-4fd8-87a1-eea8168b5573"), new DateTime(2022, 6, 11, 21, 18, 51, 832, DateTimeKind.Utc).AddTicks(9637), "gman@blackmesa.com", false, "83a6c7696be667964f0f42ac17f7fe93", "c0695027b298c139700d002f", 0, null, "gman" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: new Guid("9b003b60-6fd3-46bb-9d5f-a2527e780ec6"));

            migrationBuilder.DeleteData(
                table: "Player",
                keyColumn: "Id",
                keyValue: new Guid("e95d6bdb-42f8-4fd8-87a1-eea8168b5573"));
        }
    }
}
