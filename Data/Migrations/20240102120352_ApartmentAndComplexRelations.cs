using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentAndComplexRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentComplexId",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ApartmentComplexId",
                table: "Apartments",
                column: "ApartmentComplexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_ApartmentComplexes_ApartmentComplexId",
                table: "Apartments",
                column: "ApartmentComplexId",
                principalTable: "ApartmentComplexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_ApartmentComplexes_ApartmentComplexId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_ApartmentComplexId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ApartmentComplexId",
                table: "Apartments");
        }
    }
}
