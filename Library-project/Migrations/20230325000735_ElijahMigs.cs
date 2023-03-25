using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class ElijahMigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medias",
                columns: table => new
                {
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.mediaId);
                });

            migrationBuilder.CreateTable(
                name: "audioBooks",
                columns: table => new
                {
                    AudioBookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaId = table.Column<int>(type: "integer", nullable: false),
                    genre = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    narrator = table.Column<string>(type: "text", nullable: false),
                    author = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audioBooks", x => x.AudioBookId);
                    table.ForeignKey(
                        name: "FK_audioBooks_medias_mediaId",
                        column: x => x.mediaId,
                        principalTable: "medias",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    author = table.Column<string[]>(type: "text[]", nullable: false),
                    genres = table.Column<int[]>(type: "integer[]", nullable: false),
                    publicDate = table.Column<DateOnly>(type: "date", nullable: false),
                    pageCount = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<int>(type: "integer", nullable: false),
                    isAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookId);
                    table.ForeignKey(
                        name: "FK_books_medias_mediaId",
                        column: x => x.mediaId,
                        principalTable: "medias",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "journals",
                columns: table => new
                {
                    jouranalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaId = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    researchers = table.Column<string>(type: "text", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<int>(type: "integer", nullable: false),
                    dateReleased = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journals", x => x.jouranalId);
                    table.ForeignKey(
                        name: "FK_journals_medias_mediaId",
                        column: x => x.mediaId,
                        principalTable: "medias",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    movieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaId = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    director = table.Column<string>(type: "text", nullable: false),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    releaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.movieId);
                    table.ForeignKey(
                        name: "FK_movie_medias_mediaId",
                        column: x => x.mediaId,
                        principalTable: "medias",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audioBooks_mediaId",
                table: "audioBooks",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_books_mediaId",
                table: "books",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_journals_mediaId",
                table: "journals",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_mediaId",
                table: "movie",
                column: "mediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audioBooks");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "journals");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "medias");
        }
    }
}
