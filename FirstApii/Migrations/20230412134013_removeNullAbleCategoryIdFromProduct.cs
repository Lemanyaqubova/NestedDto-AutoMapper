using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApii.Migrations
{
    public partial class removeNullAbleCategoryIdFromProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 12, 13, 40, 13, 206, DateTimeKind.Utc).AddTicks(3785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 12, 4, 46, 1, 863, DateTimeKind.Utc).AddTicks(8357));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 12, 4, 46, 1, 863, DateTimeKind.Utc).AddTicks(8357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 12, 13, 40, 13, 206, DateTimeKind.Utc).AddTicks(3785));
        }
    }
}
