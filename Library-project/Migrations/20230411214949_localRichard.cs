using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    /// <inheritdoc />
    public partial class localRichard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employeeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "text", nullable: false),
                    mname = table.Column<string>(type: "text", nullable: false),
                    lname = table.Column<string>(type: "text", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<float>(type: "real", nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    homeaddress = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    employee = table.Column<int>(type: "integer", nullable: false),
                    supervisorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeid);
                    table.ForeignKey(
                        name: "FK_employees_employees_employee",
                        column: x => x.employee,
                        principalTable: "employees",
                        principalColumn: "employeeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historians",
                columns: table => new
                {
                    historianid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "text", nullable: false),
                    mname = table.Column<string>(type: "text", nullable: false),
                    lname = table.Column<string>(type: "text", nullable: false),
                    expertise = table.Column<string>(type: "text", nullable: false),
                    education = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historians", x => x.historianid);
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
                name: "medias",
                columns: table => new
                {
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.mediaid);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    historianid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                    table.ForeignKey(
                        name: "FK_students_historians_historianid",
                        column: x => x.historianid,
                        principalTable: "historians",
                        principalColumn: "historianid");
                });

            migrationBuilder.CreateTable(
                name: "audiobook",
                columns: table => new
                {
                    audiobookid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    genre = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    narrator = table.Column<string>(type: "text", nullable: false),
                    author = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audiobook", x => x.audiobookid);
                    table.ForeignKey(
                        name: "FK_audiobook_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    author = table.Column<string[]>(type: "text[]", nullable: false),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    publicdate = table.Column<DateOnly>(type: "date", nullable: false),
                    pagecount = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<long>(type: "bigint", nullable: false),
                    isavailable = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookid);
                    table.ForeignKey(
                        name: "FK_books_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cameras",
                columns: table => new
                {
                    cameraid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serialnumber = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    megapixels = table.Column<double>(type: "double precision", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cameras", x => x.cameraid);
                    table.ForeignKey(
                        name: "FK_cameras_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "computers",
                columns: table => new
                {
                    computerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serialnumber = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_computers", x => x.computerid);
                    table.ForeignKey(
                        name: "FK_computers_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "journals",
                columns: table => new
                {
                    jouranalid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    researchers = table.Column<string[]>(type: "text[]", nullable: false),
                    subject = table.Column<string[]>(type: "text[]", nullable: false),
                    length = table.Column<int>(type: "integer", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    isavailable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journals", x => x.jouranalid);
                    table.ForeignKey(
                        name: "FK_journals_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movieId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    director = table.Column<string>(type: "text", nullable: false),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<int>(type: "integer", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.movieId);
                    table.ForeignKey(
                        name: "FK_movies_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projectors",
                columns: table => new
                {
                    projectorid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serialnumber = table.Column<int>(type: "integer", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    lumens = table.Column<int>(type: "integer", nullable: false),
                    availibility = table.Column<bool>(type: "boolean", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectors", x => x.projectorid);
                    table.ForeignKey(
                        name: "FK_projectors_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evaluation = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<short>(type: "smallint", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    studentid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.reviewid);
                    table.ForeignKey(
                        name: "FK_reviews_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_students_studentid",
                        column: x => x.studentid,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_audiobook_mediaid",
                table: "audiobook",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_books_mediaid",
                table: "books",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_cameras_mediaid",
                table: "cameras",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_computers_mediaid",
                table: "computers",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_employees_employee",
                table: "employees",
                column: "employee");

            migrationBuilder.CreateIndex(
                name: "IX_journals_mediaid",
                table: "journals",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_movies_mediaid",
                table: "movies",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_projectors_mediaid",
                table: "projectors",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_mediaid",
                table: "reviews",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_studentid",
                table: "reviews",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_students_historianid",
                table: "students",
                column: "historianid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audiobook");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "cameras");

            migrationBuilder.DropTable(
                name: "computers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "journals");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "projectors");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "historians");
        }
    }
}
