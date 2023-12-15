using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RelationsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Tenants_TenantId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_TenantId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Apartments");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Tenants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Apartments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_ApartmentId",
                table: "Tenants",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Apartments_ApartmentId",
                table: "Tenants",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Apartments_ApartmentId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_ApartmentId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Tenants");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_TenantId",
                table: "Apartments",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Tenants_TenantId",
                table: "Apartments",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
