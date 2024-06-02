using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class migrtaion_Test11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldUserID",
                table: "ForwadMails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ForwadMails_OldUserID",
                table: "ForwadMails",
                column: "OldUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ForwadMails_AspNetUsers_OldUserID",
                table: "ForwadMails",
                column: "OldUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForwadMails_AspNetUsers_OldUserID",
                table: "ForwadMails");

            migrationBuilder.DropIndex(
                name: "IX_ForwadMails_OldUserID",
                table: "ForwadMails");

            migrationBuilder.DropColumn(
                name: "OldUserID",
                table: "ForwadMails");
        }
    }
}
