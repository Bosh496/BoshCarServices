using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoshCarServices.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypeMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "ServiceMasters");

            migrationBuilder.CreateTable(
                name: "ServiceTypeMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTypeMappings_ServiceMasters_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTypeMappings_ServiceTypeMasters_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeMappings_ServiceId",
                table: "ServiceTypeMappings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeMappings_ServiceTypeId",
                table: "ServiceTypeMappings",
                column: "ServiceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceTypeMappings");

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "ServiceMasters",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
