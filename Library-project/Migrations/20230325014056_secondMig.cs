using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class secondMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fName = table.Column<string>(type: "text", nullable: false),
                    mName = table.Column<string>(type: "text", nullable: false),
                    lName = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<float>(type: "real", nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false),
                    eMail = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    homeAddress = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    employee = table.Column<int>(type: "integer", nullable: false),
                    supervisorID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeID);
                    table.ForeignKey(
                        name: "FK_employees_employees_employee",
                        column: x => x.employee,
                        principalTable: "employees",
                        principalColumn: "employeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historians",
                columns: table => new
                {
                    historianID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fName = table.Column<string>(type: "text", nullable: false),
                    mName = table.Column<string>(type: "text", nullable: false),
                    lName = table.Column<string>(type: "text", nullable: false),
                    expertise = table.Column<string>(type: "text", nullable: false),
                    education = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historians", x => x.historianID);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.mediaId);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    historianID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_historians_historianID",
                        column: x => x.historianID,
                        principalTable: "historians",
                        principalColumn: "historianID");
                });

            migrationBuilder.CreateTable(
                name: "audiobook",
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
                    table.PrimaryKey("PK_audiobook", x => x.AudioBookId);
                    table.ForeignKey(
                        name: "FK_audiobook_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
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
                        name: "FK_books_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "computers",
                columns: table => new
                {
                    SerialNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Availibility = table.Column<bool>(type: "boolean", nullable: false),
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers", x => x.SerialNumber);
                    table.ForeignKey(
                        name: "FK_computers_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
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
                        name: "FK_journals_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movies",
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
                    table.PrimaryKey("PK_movies", x => x.movieId);
                    table.ForeignKey(
                        name: "FK_movies_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projectors",
                columns: table => new
                {
                    serialNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    lumens = table.Column<int>(type: "integer", nullable: false),
                    availibility = table.Column<bool>(type: "boolean", nullable: false),
                    mediaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectors", x => x.serialNumber);
                    table.ForeignKey(
                        name: "FK_projectors_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evaluation = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<short>(type: "smallint", nullable: false),
                    mediaId = table.Column<int>(type: "integer", nullable: false),
                    studentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.reviewID);
                    table.ForeignKey(
                        name: "FK_reviews_media_mediaId",
                        column: x => x.mediaId,
                        principalTable: "media",
                        principalColumn: "mediaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_students_studentId",
                        column: x => x.studentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audiobook_mediaId",
                table: "audiobook",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_books_mediaId",
                table: "books",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_computers_mediaId",
                table: "computers",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_employee",
                table: "employees",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_journals_mediaId",
                table: "journals",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_movies_mediaId",
                table: "movies",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_projectors_mediaId",
                table: "projectors",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_mediaId",
                table: "reviews",
                column: "mediaId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_studentId",
                table: "reviews",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_students_historianID",
                table: "students",
                column: "historianID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audiobook");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "computers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "journals");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "projectors");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "historians");
        }
    }
}
