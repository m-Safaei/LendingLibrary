using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LendingLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditUserBooksTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksBooks_AspNetUsers_ApplicationUserId",
                table: "BooksBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BooksBooks_Books_BookId",
                table: "BooksBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksBooks",
                table: "BooksBooks");

            migrationBuilder.RenameTable(
                name: "BooksBooks",
                newName: "UserBooks");

            migrationBuilder.RenameIndex(
                name: "IX_BooksBooks_BookId",
                table: "UserBooks",
                newName: "IX_UserBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BooksBooks_ApplicationUserId",
                table: "UserBooks",
                newName: "IX_UserBooks_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBooks",
                table: "UserBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUserId",
                table: "UserBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_AspNetUsers_ApplicationUserId",
                table: "UserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BookId",
                table: "UserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBooks",
                table: "UserBooks");

            migrationBuilder.RenameTable(
                name: "UserBooks",
                newName: "BooksBooks");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_BookId",
                table: "BooksBooks",
                newName: "IX_BooksBooks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBooks_ApplicationUserId",
                table: "BooksBooks",
                newName: "IX_BooksBooks_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksBooks",
                table: "BooksBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksBooks_AspNetUsers_ApplicationUserId",
                table: "BooksBooks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BooksBooks_Books_BookId",
                table: "BooksBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
