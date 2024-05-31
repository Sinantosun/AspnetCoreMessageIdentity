using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class migration_relationship_add_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Mail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Mail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrash",
                table: "Mail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MailTagsID",
                table: "Mail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MailTags",
                columns: table => new
                {
                    MailTagsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTags", x => x.MailTagsID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mail_MailTagsID",
                table: "Mail",
                column: "MailTagsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mail_MailTags_MailTagsID",
                table: "Mail",
                column: "MailTagsID",
                principalTable: "MailTags",
                principalColumn: "MailTagsID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mail_MailTags_MailTagsID",
                table: "Mail");

            migrationBuilder.DropTable(
                name: "MailTags");

            migrationBuilder.DropIndex(
                name: "IX_Mail_MailTagsID",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "IsTrash",
                table: "Mail");

            migrationBuilder.DropColumn(
                name: "MailTagsID",
                table: "Mail");
        }
    }
}
