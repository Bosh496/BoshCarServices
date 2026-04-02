using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoshCarServices.Migrations
{
    /// <inheritdoc />
    public partial class AddFileStorageToServiceMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "ServiceMasters",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ServiceMasters",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "ServiceMasters",
                type: "longblob",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ServiceMasters");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "ServiceMasters");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ServiceMasters",
                newName: "FileUrl");
        }
    }
}
