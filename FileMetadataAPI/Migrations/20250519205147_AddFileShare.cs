using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FileMetadataAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFileShare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares");

            migrationBuilder.DropIndex(
                name: "IX_FileShares_FileId",
                table: "FileShares");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FileShares",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares",
                columns: new[] { "FileId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FileShares",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileShares",
                table: "FileShares",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileShares_FileId",
                table: "FileShares",
                column: "FileId");
        }
    }
}
