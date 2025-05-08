using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "ProductReviews",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "ProductRatings",
                type: "float",
                nullable: false,
                defaultValue: 1.0,
                comment: "Rating value from 1 to 5",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1,
                oldComment: "Rating value from 1 to 5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ProductReviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ProductRatings",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "Rating value from 1 to 5",
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 1.0,
                oldComment: "Rating value from 1 to 5");
        }
    }
}
