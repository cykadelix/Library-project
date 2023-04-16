using System;
using System.Collections.Generic;
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
                name: "employees",
                columns: table => new
                {
                    employeeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "text", nullable: true),
                    mname = table.Column<string>(type: "text", nullable: true),
                    lname = table.Column<string>(type: "text", nullable: true),
                    position = table.Column<string>(type: "text", nullable: true),
                    salary = table.Column<float>(type: "real", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    password = table.Column<string>(type: "text", nullable: true),
                    homeaddress = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.employeeid);
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
                name: "rooms",
                columns: table => new
                {
                    room_number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    features = table.Column<string>(type: "text", nullable: true),
                    availabilty = table.Column<List<DateTime>>(type: "timestamp with time zone[]", nullable: true),
                    reserved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.room_number);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userdtos",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "audiobooks",
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
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audiobooks", x => x.audiobookid);
                    table.ForeignKey(
                        name: "FK_audiobooks_medias_mediaid",
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
                    title = table.Column<string>(type: "text", nullable: true),
                    author = table.Column<string>(type: "text", nullable: true),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    publicdate = table.Column<DateOnly>(type: "date", nullable: false),
                    pagecount = table.Column<int>(type: "integer", nullable: false),
                    isbn = table.Column<long>(type: "bigint", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false),
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
                    serialnumber = table.Column<string>(type: "text", nullable: true),
                    brand = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
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
                    serialnumber = table.Column<string>(type: "text", nullable: true),
                    brand = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
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
                    journalid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    researchers = table.Column<string>(type: "text", nullable: true),
                    subject = table.Column<string>(type: "text", nullable: true),
                    length = table.Column<int>(type: "integer", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journals", x => x.journalid);
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
                    movieid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mediaid = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    director = table.Column<string>(type: "text", nullable: true),
                    genres = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<int>(type: "integer", nullable: false),
                    releasedate = table.Column<DateOnly>(type: "date", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.movieid);
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
                    serialnumber = table.Column<string>(type: "text", nullable: true),
                    brand = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    lumens = table.Column<int>(type: "integer", nullable: false),
                    availability = table.Column<bool>(type: "boolean", nullable: false),
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
                name: "activities",
                columns: table => new
                {
                    activity_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    length = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    activity_type = table.Column<string>(type: "text", nullable: true),
                    room_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.activity_id);
                    table.ForeignKey(
                        name: "FK_activities_rooms_room_number",
                        column: x => x.room_number,
                        principalTable: "rooms",
                        principalColumn: "room_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "checkouts",
                columns: table => new
                {
                    checkoutid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    checkoutdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    returndate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    studentid = table.Column<int>(type: "integer", nullable: false),
                    mediaid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkouts", x => x.checkoutid);
                    table.ForeignKey(
                        name: "FK_checkouts_medias_mediaid",
                        column: x => x.mediaid,
                        principalTable: "medias",
                        principalColumn: "mediaid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historians",
                columns: table => new
                {
                    historianid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "text", nullable: true),
                    mname = table.Column<string>(type: "text", nullable: true),
                    lname = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    expertise = table.Column<string>(type: "text", nullable: true),
                    education = table.Column<string>(type: "text", nullable: true),
                    library_card_number = table.Column<int>(type: "integer", nullable: false),
                    studentlibrary_card_number = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historians", x => x.historianid);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    library_card_number = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "text", nullable: true),
                    mname = table.Column<string>(type: "text", nullable: true),
                    lname = table.Column<string>(type: "text", nullable: true),
                    homeaddress = table.Column<string>(type: "text", nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true),
                    overdue_fees = table.Column<float>(type: "real", nullable: false),
                    historianshistorianid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.library_card_number);
                    table.ForeignKey(
                        name: "FK_students_historians_historianshistorianid",
                        column: x => x.historianshistorianid,
                        principalTable: "historians",
                        principalColumn: "historianid");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evaluation = table.Column<string>(type: "text", nullable: true),
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
                        principalColumn: "library_card_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activities_room_number",
                table: "activities",
                column: "room_number");

            migrationBuilder.CreateIndex(
                name: "IX_audiobooks_mediaid",
                table: "audiobooks",
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
                name: "IX_checkouts_mediaid",
                table: "checkouts",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_checkouts_studentid",
                table: "checkouts",
                column: "studentid");

            migrationBuilder.CreateIndex(
                name: "IX_computers_mediaid",
                table: "computers",
                column: "mediaid");

            migrationBuilder.CreateIndex(
                name: "IX_historians_studentlibrary_card_number",
                table: "historians",
                column: "studentlibrary_card_number");

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
                name: "IX_students_historianshistorianid",
                table: "students",
                column: "historianshistorianid");

            migrationBuilder.AddForeignKey(
                name: "FK_checkouts_students_studentid",
                table: "checkouts",
                column: "studentid",
                principalTable: "students",
                principalColumn: "library_card_number",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historians_students_studentlibrary_card_number",
                table: "historians",
                column: "studentlibrary_card_number",
                principalTable: "students",
                principalColumn: "library_card_number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_historians_students_studentlibrary_card_number",
                table: "historians");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "audiobooks");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "cameras");

            migrationBuilder.DropTable(
                name: "checkouts");

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
                name: "user");

            migrationBuilder.DropTable(
                name: "userdtos");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "historians");
        }
    }
}
