using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditUserId = table.Column<int>(type: "int", nullable: false),
                    OperUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leave_Users_AuditUserId",
                        column: x => x.AuditUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leave_Users_OperUserId",
                        column: x => x.OperUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leave_AuditUserId",
                table: "Leave",
                column: "AuditUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leave_OperUserId",
                table: "Leave",
                column: "OperUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leave");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
