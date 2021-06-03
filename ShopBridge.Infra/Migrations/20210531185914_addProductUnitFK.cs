using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridge.Infra.Migrations
{
    public partial class addProductUnitFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "UnitId",
                schema: "dbo",
                table: "mstProduct",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_mstProduct_UnitId",
                schema: "dbo",
                table: "mstProduct",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_mstProduct_mstUnit_UnitId",
                schema: "dbo",
                table: "mstProduct",
                column: "UnitId",
                principalSchema: "dbo",
                principalTable: "mstUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mstProduct_mstUnit_UnitId",
                schema: "dbo",
                table: "mstProduct");

            migrationBuilder.DropIndex(
                name: "IX_mstProduct_UnitId",
                schema: "dbo",
                table: "mstProduct");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                schema: "dbo",
                table: "mstProduct",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
