using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_replyMails_AspNetUsers_ReplyReciverId",
                table: "replyMails");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "replyMails");

            migrationBuilder.RenameColumn(
                name: "ReplyReciverId",
                table: "replyMails",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_replyMails_ReplyReciverId",
                table: "replyMails",
                newName: "IX_replyMails_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_replyMails_AspNetUsers_AppUserId",
                table: "replyMails",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_replyMails_AspNetUsers_AppUserId",
                table: "replyMails");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "replyMails",
                newName: "ReplyReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_replyMails_AppUserId",
                table: "replyMails",
                newName: "IX_replyMails_ReplyReciverId");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "replyMails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_replyMails_AspNetUsers_ReplyReciverId",
                table: "replyMails",
                column: "ReplyReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
