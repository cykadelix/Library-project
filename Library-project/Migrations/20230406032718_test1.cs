using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_media_mediaId",
                table: "books");

            migrationBuilder.RenameColumn(
                name: "datereleased",
                table: "journals",
                newName: "releasedate");

            migrationBuilder.RenameColumn(
                name: "publicDate",
                table: "books",
                newName: "publicdate");

            migrationBuilder.RenameColumn(
                name: "pageCount",
                table: "books",
                newName: "pagecount");

            migrationBuilder.RenameColumn(
                name: "mediaId",
                table: "books",
                newName: "mediaid");

            migrationBuilder.RenameColumn(
                name: "bookId",
                table: "books",
                newName: "bookid");

            migrationBuilder.RenameIndex(
                name: "IX_books_mediaId",
                table: "books",
                newName: "IX_books_mediaid");

            migrationBuilder.AlterColumn<int>(
                name: "length",
                table: "movies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<string[]>(
                name: "subject",
                table: "journals",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string[]>(
                name: "researchers",
                table: "journals",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "isavailable",
                table: "journals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_books_media_mediaid",
                table: "books",
                column: "mediaid",
                principalTable: "media",
                principalColumn: "mediaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_media_mediaid",
                table: "books");

            migrationBuilder.DropColumn(
                name: "isavailable",
                table: "journals");

            migrationBuilder.RenameColumn(
                name: "releasedate",
                table: "journals",
                newName: "datereleased");

            migrationBuilder.RenameColumn(
                name: "publicdate",
                table: "books",
                newName: "publicDate");

            migrationBuilder.RenameColumn(
                name: "pagecount",
                table: "books",
                newName: "pageCount");

            migrationBuilder.RenameColumn(
                name: "mediaid",
                table: "books",
                newName: "mediaId");

            migrationBuilder.RenameColumn(
                name: "bookid",
                table: "books",
                newName: "bookId");

            migrationBuilder.RenameIndex(
                name: "IX_books_mediaid",
                table: "books",
                newName: "IX_books_mediaId");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "length",
                table: "movies",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "journals",
                type: "text",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AlterColumn<string>(
                name: "researchers",
                table: "journals",
                type: "text",
                nullable: false,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AddForeignKey(
                name: "FK_books_media_mediaId",
                table: "books",
                column: "mediaId",
                principalTable: "media",
                principalColumn: "mediaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
