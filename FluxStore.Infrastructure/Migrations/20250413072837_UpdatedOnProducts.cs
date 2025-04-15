using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOnProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings");

            migrationBuilder.RenameTable(
                name: "ProductReviews",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "ProductRatings",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReviews_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_ProductId",
                table: "Ratings",
                newName: "IX_Ratings_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Products_ProductId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ProductReviews");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ProductRatings");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "ProductReviews",
                newName: "IX_ProductReviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProductId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Products_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
