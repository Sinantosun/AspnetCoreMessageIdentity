using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class initizate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_replyMails_Mail_ReplyMailsID",
                table: "replyMails");

            migrationBuilder.AddForeignKey(
                name: "FK_replyMails_Mail_ReplyMailsID",
                table: "replyMails",
                column: "ReplyMailsID",
                principalTable: "Mail",
                principalColumn: "MailsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_replyMails_Mail_ReplyMailsID",
                table: "replyMails");

            migrationBuilder.AddForeignKey(
                name: "FK_replyMails_Mail_ReplyMailsID",
                table: "replyMails",
                column: "ReplyMailsID",
                principalTable: "Mail",
                principalColumn: "MailsId");
        }
    }
}
