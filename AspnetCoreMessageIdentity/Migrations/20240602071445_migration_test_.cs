using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMessageIdentity.Migrations
{
    /// <inheritdoc />
    public partial class migration_test_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReply",
                table: "Mail",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReply",
                table: "Mail");
        }
    }
}
