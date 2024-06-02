using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class migration_test111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForwadMails",
                columns: table => new
                {
                    ForwadMailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailsId = table.Column<int>(type: "int", nullable: false),
                    ReciverID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForwadMails", x => x.ForwadMailsID);
                    table.ForeignKey(
                        name: "FK_ForwadMails_AspNetUsers_ReciverID",
                        column: x => x.ReciverID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForwadMails_AspNetUsers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForwadMails_Mail_MailsId",
                        column: x => x.MailsId,
                        principalTable: "Mail",
                        principalColumn: "MailsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForwadMails_MailsId",
                table: "ForwadMails",
                column: "MailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ForwadMails_ReciverID",
                table: "ForwadMails",
                column: "ReciverID");

            migrationBuilder.CreateIndex(
                name: "IX_ForwadMails_SenderID",
                table: "ForwadMails",
                column: "SenderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForwadMails");
        }
    }
}
