using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseEncryption.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEncryptionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Users");
        }
    }
}
