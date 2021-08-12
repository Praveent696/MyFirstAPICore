using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddInsuranceEntities1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypes_VehicleTypes_ParentId",
                table: "VehicleTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "VehicleTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypes_VehicleTypes_ParentId",
                table: "VehicleTypes",
                column: "ParentId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleTypes_VehicleTypes_ParentId",
                table: "VehicleTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "VehicleTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleTypes_VehicleTypes_ParentId",
                table: "VehicleTypes",
                column: "ParentId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
