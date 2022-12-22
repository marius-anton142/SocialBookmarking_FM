using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialBookmarking_FM.Data.Migrations
{
    public partial class Collections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookmarkCollectionId",
                table: "Bookmarks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookmarkCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    BookmarkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionCategory", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Votes",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        BookmarkId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Votes", x => new { x.UserId, x.BookmarkId });
            //    });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookmarkCollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collection_BookmarkCollection_BookmarkCollectionId",
                        column: x => x.BookmarkCollectionId,
                        principalTable: "BookmarkCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Collection_CollectionCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CollectionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_BookmarkCollectionId",
                table: "Bookmarks",
                column: "BookmarkCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkCollection_BookmarkId_CollectionId",
                table: "BookmarkCollection",
                columns: new[] { "BookmarkId", "CollectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collection_BookmarkCollectionId",
                table: "Collection",
                column: "BookmarkCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_CategoryId",
                table: "Collection",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_BookmarkCollection_BookmarkCollectionId",
                table: "Bookmarks",
                column: "BookmarkCollectionId",
                principalTable: "BookmarkCollection",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_BookmarkCollection_BookmarkCollectionId",
                table: "Bookmarks");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "BookmarkCollection");

            migrationBuilder.DropTable(
                name: "CollectionCategory");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_BookmarkCollectionId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "BookmarkCollectionId",
                table: "Bookmarks");
        }
    }
}
