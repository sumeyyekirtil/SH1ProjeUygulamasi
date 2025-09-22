using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SH1ProjeUygulamasi.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsHomeEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 9, 22, 21, 48, 57, 592, DateTimeKind.Local).AddTicks(6453), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 9, 11, 22, 25, 56, 234, DateTimeKind.Local).AddTicks(9727), new Guid("a955e9d8-d7e8-48cb-9648-fd7e34cd635d") });
        }
    }
}
