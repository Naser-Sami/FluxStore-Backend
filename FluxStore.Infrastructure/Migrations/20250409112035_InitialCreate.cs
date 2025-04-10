using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    IconName = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR(36)", nullable: false),
                    UserName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    RefreshToken = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "VARCHAR(128)", maxLength: 128, nullable: true),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
