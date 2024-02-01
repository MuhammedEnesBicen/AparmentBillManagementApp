using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MessageAndChatRoomTablesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Messages_LastSeenMessageId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Tenants_TenantId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_TenantId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_LastSeenMessageId",
                table: "ChatRooms");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Messages",
                newName: "ChatRoomId");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "ChatRooms",
                newName: "TenantId");

            migrationBuilder.AlterColumn<int>(
                name: "LastSeenMessageId",
                table: "ChatRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_LastSeenMessageId",
                table: "ChatRooms",
                column: "LastSeenMessageId",
                unique: true,
                filter: "[LastSeenMessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_TenantId",
                table: "ChatRooms",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Messages_LastSeenMessageId",
                table: "ChatRooms",
                column: "LastSeenMessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Tenants_TenantId",
                table: "ChatRooms",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Messages_LastSeenMessageId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Tenants_TenantId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_LastSeenMessageId",
                table: "ChatRooms");

            migrationBuilder.DropIndex(
                name: "IX_ChatRooms_TenantId",
                table: "ChatRooms");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "Messages",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "ChatRooms",
                newName: "User");

            migrationBuilder.AlterColumn<int>(
                name: "LastSeenMessageId",
                table: "ChatRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TenantId",
                table: "Messages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_LastSeenMessageId",
                table: "ChatRooms",
                column: "LastSeenMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Messages_LastSeenMessageId",
                table: "ChatRooms",
                column: "LastSeenMessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Tenants_TenantId",
                table: "Messages",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
