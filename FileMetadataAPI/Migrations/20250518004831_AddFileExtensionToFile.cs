using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileMetadataAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFileExtensionToFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "files",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "files");
        }
    }
}
