using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class tableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position = table.Column<string>(type: "text", nullable: true),
                    salary = table.Column<float>(type: "real", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    employee = table.Column<int>(type: "integer", nullable: true),
                    supervisorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employeeId);
                    table.ForeignKey(
                        name: "FK_employee_employee_employee",
                        column: x => x.employee,
                        principalTable: "employee",
                        principalColumn: "employeeId");
                });

            migrationBuilder.CreateTable(
                name: "historian",
                columns: table => new
                {
                    historian_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    expertise = table.Column<string>(type: "text", nullable: true),
                    education = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historian", x => x.historian_id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    r = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.mediaid);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    library_card_number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    overdue_fees = table.Column<float>(type: "real", nullable: false),
                    historian_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.library_card_number);
                    table.ForeignKey(
                        name: "FK_student_historian_historian_id",
                        column: x => x.historian_id,
                        principalTable: "historian",
                        principalColumn: "historian_id");
                });

            migrationBuilder.CreateTable(
                name: "audiobook",
                columns: table => new
                {
                    audiobookid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    genre = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    narrator = table.Column<string>(type: "text", nullable: true),
                    author = table.Column<string>(type: "text", nullable: true),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audiobook", x => x.audiobookid);
                    table.ForeignKey(
                        name: "FK_audiobook_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    bookid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    copynumber = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    author = table.Column<string[]>(type: "text[]", nullable: true),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    publicdate = table.Column<DateOnly>(type: "date", nullable: false),
                    pagecount = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<long>(type: "bigint", nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.bookid);
                    table.ForeignKey(
                        name: "FK_book_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "computer",
                columns: table => new
                {
                    serialnumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    availibility = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computer", x => x.serialnumber);
                    table.ForeignKey(
                        name: "FK_computer_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "journal",
                columns: table => new
                {
                    jouranalid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    researchers = table.Column<string[]>(type: "text[]", nullable: true),
                    subject = table.Column<string[]>(type: "text[]", nullable: true),
                    length = table.Column<int>(type: "integer", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    availabilty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journal", x => x.jouranalid);
                    table.ForeignKey(
                        name: "FK_journal_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    movieid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    director = table.Column<string>(type: "text", nullable: true),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    availabilty = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.movieid);
                    table.ForeignKey(
                        name: "FK_movie_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projector",
                columns: table => new
                {
                    serialnumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    lumens = table.Column<int>(type: "integer", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projector", x => x.serialnumber);
                    table.ForeignKey(
                        name: "FK_projector_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evaluation = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    library_card_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_review_media_mediaid",
                        column: x => x.mediaid,
                        principalTable: "media",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_review_student_library_card_number",
                        column: x => x.library_card_number,
                        principalTable: "student",
                        principalColumn: "library_card_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audiobook_mediaid",
                table: "audiobook",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_book_mediaid",
                table: "book",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_computer_mediaid",
                table: "computer",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employee",
                table: "employee",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_journal_mediaid",
                table: "journal",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_movie_mediaid",
                table: "movie",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_projector_mediaid",
                table: "projector",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_review_library_card_number",
                table: "review",
                column: "library_card_number");

            migrationBuilder.CreateIndex(
                name: "IX_review_mediaid",
                table: "review",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_student_historian_id",
                table: "student",
                column: "historian_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audiobook");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "computer");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "journal");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "projector");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "historian");
        }
    }
}
