using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class all : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Floor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Asile = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Floor);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.mediaId);
                });

            migrationBuilder.CreateTable(
                name: "AudioBook",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mediaid = table.Column<int>(name: "Media.id", type: "integer", nullable: false),
                    Genre = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Narrrator = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioBook", x => x.AudioBookId);
                    table.ForeignKey(
                        name: "FK_AudioBook_Media_Media.id",
                        column: x => x.Mediaid,
                        principalTable: "Media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mediaid = table.Column<int>(name: "Media.id", type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string[]>(type: "text[]", nullable: false),
                    Genres = table.Column<int[]>(type: "integer[]", nullable: false),
                    PublicDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PageCount = table.Column<int>(type: "integer", nullable: false),
                    ISBN = table.Column<int>(type: "integer", nullable: false),
                    isAvailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Media_Media.id",
                        column: x => x.Mediaid,
                        principalTable: "Media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mediaid = table.Column<int>(name: "Media.id", type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Researchers = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    DateReleased = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_Journal_Media_Media.id",
                        column: x => x.Mediaid,
                        principalTable: "Media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mediaid = table.Column<int>(name: "Media.id", type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Genres = table.Column<int>(type: "integer", nullable: false),
                    Length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ReleasDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movie_Media_Media.id",
                        column: x => x.Mediaid,
                        principalTable: "Media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudioBook_Media.id",
                table: "AudioBook",
                column: "Media.id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Media.id",
                table: "Book",
                column: "Media.id");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_Media.id",
                table: "Journal",
                column: "Media.id");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_Media.id",
                table: "Movie",
                column: "Media.id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioBook");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
