using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library_project;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Audiobook> Audiobooks { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Camera> Cameras { get; set; }

    public virtual DbSet<Checkout> Checkouts { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Historian> Historians { get; set; }

    public virtual DbSet<Journal> Journals { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Projector> Projectors { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseNpgsql("Host=azurelibrarydatabase.postgres.database.azure.com;Database=Library;Username=chavemm;Password=Postgres-2023!");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audiobook>(entity =>
        {
            entity.HasKey(e => e.AudioBookId).HasName("AudioBook_pkey");

            entity.ToTable("audiobook");

            entity.HasIndex(e => e.AudioBookId, "fki_AudioBook_fkey");

            entity.Property(e => e.AudioBookId)
                .HasDefaultValueSql("nextval('\"AudioBook_AudioBookID_seq\"'::regclass)")
                .HasColumnName("AudioBookID");
            entity.Property(e => e.Author).HasColumnType("character varying");
            entity.Property(e => e.Genre).HasColumnType("character varying[]");
            entity.Property(e => e.Narrator).HasColumnType("character varying");

            entity.HasOne(d => d.AudioBook).WithOne(p => p.Audiobook)
                .HasForeignKey<Audiobook>(d => d.AudioBookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AudioBook_fkey");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.Isbn }).HasName("Book_pkey");

            entity.ToTable("book");

            entity.HasIndex(e => e.BookId, "fki_Media_id");

            entity.HasIndex(e => e.BookId, "fki_book_fkey");

            entity.HasIndex(e => e.BookId, "fki_media_fkey");

            entity.Property(e => e.BookId)
                .HasDefaultValueSql("nextval('\"Book_BookID_seq\"'::regclass)")
                .HasColumnName("BookID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Unknown'::character varying");
            entity.Property(e => e.Genre).HasColumnType("character varying(50)[]");
            entity.Property(e => e.YearPublished).HasColumnName("Year_Published");

            entity.HasOne(d => d.BookNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_fkey");
        });

        modelBuilder.Entity<Camera>(entity =>
        {
            entity.HasKey(e => e.Serialnumber).HasName("camera_pkey");

            entity.ToTable("camera");

            entity.HasIndex(e => e.Serialnumber, "fki_Camera_Fkey");

            entity.HasIndex(e => e.Serialnumber, "fki_camera_fkey");

            entity.Property(e => e.Serialnumber)
                .ValueGeneratedOnAdd()
                .HasColumnName("serialnumber");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lumens).HasColumnName("lumens");

            entity.HasOne(d => d.SerialnumberNavigation).WithOne(p => p.Camera)
                .HasForeignKey<Camera>(d => d.Serialnumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("camera_fkey");
        });

        modelBuilder.Entity<Checkout>(entity =>
        {
            entity.HasKey(e => e.CheckoutId).HasName("checkout_pkey");

            entity.ToTable("Checkout");

            entity.HasIndex(e => e.CheckoutId, "fki_checkout_fkey");

            entity.HasIndex(e => e.Studentid, "fki_studentid_fkey");

            entity.Property(e => e.CheckoutId)
                .ValueGeneratedNever()
                .HasColumnName("checkout_id");
            entity.Property(e => e.Checkoutdt).HasColumnName("checkoutdt");
            entity.Property(e => e.LateFee).HasColumnName("late_fee");
            entity.Property(e => e.ObjectType)
                .HasMaxLength(20)
                .HasColumnName("object_type");
            entity.Property(e => e.ReTurn).HasColumnName("re_turn");
            entity.Property(e => e.Studentid).HasColumnName("studentid");

            entity.HasOne(d => d.CheckoutNavigation).WithOne(p => p.Checkout)
                .HasForeignKey<Checkout>(d => d.CheckoutId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("checkout_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Checkouts)
                .HasForeignKey(d => d.Studentid)
                .HasConstraintName("studentid_fkey");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.SerialNumber).HasName("computer_pkey");

            entity.ToTable("computer");

            entity.HasIndex(e => e.SerialNumber, "fki_Computer_Fkey");

            entity.Property(e => e.SerialNumber).HasDefaultValueSql("nextval('computer_serialnumber_seq'::regclass)");
            entity.Property(e => e.Brand).HasMaxLength(50);

            entity.HasOne(d => d.SerialNumberNavigation).WithOne(p => p.Computer)
                .HasForeignKey<Computer>(d => d.SerialNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Computer_Fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.ToTable("employee");

            entity.HasIndex(e => e.SupervisorId, "fki_SupervisorID");

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("nextval('\"Employee_EmployeeID_seq\"'::regclass)")
                .HasColumnName("employee_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Homeaddress)
                .HasMaxLength(200)
                .HasColumnName("homeaddress");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(11)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.SupervisorId)
                .HasDefaultValueSql("nextval('\"Employee_SupervisorID_seq\"'::regclass)")
                .HasColumnName("supervisor_id");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SupervisorID");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("Event_pkey");

            entity.ToTable("event");

            entity.HasIndex(e => e.RoomNumber, "fki_RoomNumber");

            entity.Property(e => e.EventId)
                .HasDefaultValueSql("nextval('\"Event_EventID_seq\"'::regclass)")
                .HasColumnName("EventID");
            entity.Property(e => e.EventType).HasMaxLength(255);
            entity.Property(e => e.RoomNumber).HasDefaultValueSql("nextval('\"Event_RoomNumber_seq\"'::regclass)");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RoomNumber");
        });

        modelBuilder.Entity<Historian>(entity =>
        {
            entity.HasKey(e => e.HistorianId).HasName("Historian_pkey");

            entity.ToTable("historian");

            entity.Property(e => e.HistorianId)
                .HasDefaultValueSql("nextval('\"Historian_HistorianID_seq\"'::regclass)")
                .HasColumnName("historian_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .HasColumnName("education");
            entity.Property(e => e.Expertise)
                .HasMaxLength(50)
                .HasColumnName("expertise");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
        });

        modelBuilder.Entity<Journal>(entity =>
        {
            entity.HasKey(e => e.Journalid).HasName("Journal_pkey");

            entity.ToTable("journal");

            entity.HasIndex(e => e.Journalid, "fki_Journa_fkey");

            entity.Property(e => e.Journalid)
                .HasDefaultValueSql("nextval('\"Journal_JournalD_seq\"'::regclass)")
                .HasColumnName("journalid");
            entity.Property(e => e.Isavailable).HasColumnName("isavailable");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Publishngdate).HasColumnName("publishngdate");
            entity.Property(e => e.Reaserchers)
                .HasColumnType("character varying[]")
                .HasColumnName("reaserchers");
            entity.Property(e => e.Subject)
                .HasColumnType("character varying[]")
                .HasColumnName("subject");

            entity.HasOne(d => d.JournalNavigation).WithOne(p => p.Journal)
                .HasForeignKey<Journal>(d => d.Journalid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Journa_fkey");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.Mediaid).HasName("Media_pkey");

            entity.ToTable("media");

            entity.Property(e => e.Mediaid)
                .HasDefaultValueSql("nextval('\"Media_MediaID_seq\"'::regclass)")
                .HasColumnName("mediaid");
            entity.Property(e => e.MediaType)
                .HasColumnType("character varying")
                .HasColumnName("media_type");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Movieid).HasName("Movie_pkey");

            entity.ToTable("movie");

            entity.HasIndex(e => e.Movieid, "fki_Movie_Fkey");

            entity.Property(e => e.Movieid)
                .HasDefaultValueSql("nextval('\"Movie_Movie_ID_seq\"'::regclass)")
                .HasColumnName("movieid");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.Director)
                .HasColumnType("character varying")
                .HasColumnName("director");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Releasedate).HasColumnName("releasedate");

            entity.HasOne(d => d.MovieNavigation).WithOne(p => p.Movie)
                .HasForeignKey<Movie>(d => d.Movieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Movie_Fkey");
        });

        modelBuilder.Entity<Projector>(entity =>
        {
            entity.HasKey(e => e.Serialnumber).HasName("projector_pkey");

            entity.ToTable("projector");

            entity.HasIndex(e => e.Serialnumber, "fki_Project_Fkey");

            entity.Property(e => e.Serialnumber)
                .ValueGeneratedOnAdd()
                .HasColumnName("serialnumber");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lumens).HasColumnName("lumens");

            entity.HasOne(d => d.SerialnumberNavigation).WithOne(p => p.Projector)
                .HasForeignKey<Projector>(d => d.Serialnumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Project_Fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("review_pkey");

            entity.ToTable("Review");

            entity.HasIndex(e => e.Authorid, "fki_authorid_fkey");

            entity.HasIndex(e => e.Mediaid, "fki_mediaid_fkey");

            entity.Property(e => e.ReviewId)
                .ValueGeneratedNever()
                .HasColumnName("review_id");
            entity.Property(e => e.Authorid)
                .HasDefaultValueSql("nextval('review_authorid_seq'::regclass)")
                .HasColumnName("authorid");
            entity.Property(e => e.Evaluation)
                .HasMaxLength(1000)
                .HasColumnName("evaluation");
            entity.Property(e => e.Mediaid)
                .HasDefaultValueSql("nextval('review_mediaid_seq'::regclass)")
                .HasColumnName("mediaid");
            entity.Property(e => e.ObjectType)
                .HasMaxLength(20)
                .HasColumnName("object_type");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Author).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Authorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authorid_fkey");

            entity.HasOne(d => d.Media).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Mediaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mediaid_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("Room_pkey");

            entity.ToTable("room");

            entity.Property(e => e.RoomNumber).HasDefaultValueSql("nextval('\"Room_RoomNumber_seq\"'::regclass)");
            entity.Property(e => e.Capacity).HasMaxLength(9);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Librarycardnumber).HasName("Student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.Librarycardnumber)
                .HasDefaultValueSql("nextval('\"Student_LibraryCardNumber_seq\"'::regclass)")
                .HasColumnName("librarycardnumber");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasColumnType("character varying")
                .HasColumnName("middlename");
            entity.Property(e => e.Overduefees).HasColumnName("overduefees");
            entity.Property(e => e.Phonenumber)
                .HasColumnType("character varying")
                .HasColumnName("phonenumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
