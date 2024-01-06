using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ManagerAndAComplexRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentComplexId",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ApartmentComplexId",
                table: "Managers",
                column: "ApartmentComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_ApartmentComplexes_ApartmentComplexId",
                table: "Managers",
                column: "ApartmentComplexId",
                principalTable: "ApartmentComplexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_ApartmentComplexes_ApartmentComplexId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_ApartmentComplexId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ApartmentComplexId",
                table: "Managers");
        }
    }
}
