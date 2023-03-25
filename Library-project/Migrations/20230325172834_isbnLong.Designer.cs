﻿// <auto-generated />
using System;
using Library_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library_project.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230325172834_isbnLong")]
    partial class isbnLong
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Library_project.Models.audiobook", b =>
                {
                    b.Property<int>("AudioBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AudioBookId"));

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("availability")
                        .HasColumnType("boolean");

                    b.Property<int>("genre")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("length")
                        .HasColumnType("time without time zone");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.Property<string>("narrator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AudioBookId");

                    b.HasIndex("mediaId");

                    b.ToTable("audiobook");
                });

            modelBuilder.Entity("Library_project.Models.book", b =>
                {
                    b.Property<int>("bookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("bookId"));

                    b.Property<string[]>("author")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int[]>("genres")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("boolean");

                    b.Property<long>("isbn")
                        .HasColumnType("bigint");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.Property<int>("pageCount")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("publicDate")
                        .HasColumnType("date");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("bookId");

                    b.HasIndex("mediaId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("Library_project.Models.computer", b =>
                {
                    b.Property<int>("SerialNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SerialNumber"));

                    b.Property<bool>("Availibility")
                        .HasColumnType("boolean");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.HasKey("SerialNumber");

                    b.HasIndex("mediaId");

                    b.ToTable("computers");
                });

            modelBuilder.Entity("Library_project.Models.employee", b =>
                {
                    b.Property<int>("employeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("employeeID"));

                    b.Property<short>("age")
                        .HasColumnType("smallint");

                    b.Property<string>("eMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("employee")
                        .HasColumnType("integer");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("homeAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("salary")
                        .HasColumnType("real");

                    b.Property<int>("supervisorID")
                        .HasColumnType("integer");

                    b.HasKey("employeeID");

                    b.HasIndex("employee");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("Library_project.Models.historian", b =>
                {
                    b.Property<int>("historianID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("historianID"));

                    b.Property<short>("age")
                        .HasColumnType("smallint");

                    b.Property<string>("education")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("expertise")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("mName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("historianID");

                    b.ToTable("historians");
                });

            modelBuilder.Entity("Library_project.Models.journal", b =>
                {
                    b.Property<int>("jouranalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("jouranalId"));

                    b.Property<DateOnly>("dateReleased")
                        .HasColumnType("date");

                    b.Property<int>("length")
                        .HasColumnType("integer");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.Property<string>("researchers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("jouranalId");

                    b.HasIndex("mediaId");

                    b.ToTable("journals");
                });

            modelBuilder.Entity("Library_project.Models.media", b =>
                {
                    b.Property<int>("mediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("mediaId"));

                    b.HasKey("mediaId");

                    b.ToTable("media");
                });

            modelBuilder.Entity("Library_project.Models.movie", b =>
                {
                    b.Property<int>("movieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("movieId"));

                    b.Property<bool>("availability")
                        .HasColumnType("boolean");

                    b.Property<string>("director")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("genres")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("length")
                        .HasColumnType("time without time zone");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.Property<int>("rating")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("releaseDate")
                        .HasColumnType("date");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("movieId");

                    b.HasIndex("mediaId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("Library_project.Models.projector", b =>
                {
                    b.Property<int>("serialNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("serialNumber"));

                    b.Property<bool>("availibility")
                        .HasColumnType("boolean");

                    b.Property<string>("brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("lumens")
                        .HasColumnType("integer");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.HasKey("serialNumber");

                    b.HasIndex("mediaId");

                    b.ToTable("projectors");
                });

            modelBuilder.Entity("Library_project.Models.review", b =>
                {
                    b.Property<int>("reviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("reviewID"));

                    b.Property<string>("evaluation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("mediaId")
                        .HasColumnType("integer");

                    b.Property<short>("rating")
                        .HasColumnType("smallint");

                    b.Property<int>("studentId")
                        .HasColumnType("integer");

                    b.HasKey("reviewID");

                    b.HasIndex("mediaId");

                    b.HasIndex("studentId");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("Library_project.Models.student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("historianID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("historianID");

                    b.ToTable("students");
                });

            modelBuilder.Entity("Library_project.Models.audiobook", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.book", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.computer", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.employee", b =>
                {
                    b.HasOne("Library_project.Models.employee", "supervisor")
                        .WithMany()
                        .HasForeignKey("employee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("supervisor");
                });

            modelBuilder.Entity("Library_project.Models.journal", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.movie", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.projector", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");
                });

            modelBuilder.Entity("Library_project.Models.review", b =>
                {
                    b.HasOne("Library_project.Models.media", "media")
                        .WithMany()
                        .HasForeignKey("mediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library_project.Models.student", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("media");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Library_project.Models.student", b =>
                {
                    b.HasOne("Library_project.Models.historian", null)
                        .WithMany("studentsToSee")
                        .HasForeignKey("historianID");
                });

            modelBuilder.Entity("Library_project.Models.historian", b =>
                {
                    b.Navigation("studentsToSee");
                });
#pragma warning restore 612, 618
        }
    }
}
