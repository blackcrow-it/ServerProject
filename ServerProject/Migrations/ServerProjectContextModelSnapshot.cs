﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerProject.Models;

namespace ServerProject.Migrations
{
    [DbContext(typeof(ServerProjectContext))]
    partial class ServerProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServerProject.Models.Accounts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Salt");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ServerProject.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ServerProject.Models.Credential", b =>
                {
                    b.Property<string>("AccessToken")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("ExpiredAt");

                    b.Property<int>("OwnerId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("AccessToken");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("ServerProject.Models.GradeCourse", b =>
                {
                    b.Property<int>("GradeId");

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("GradeId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("GradeCourse");
                });

            modelBuilder.Entity("ServerProject.Models.Grades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("ServerProject.Models.Informations", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("AccountId");

                    b.ToTable("Informations");
                });

            modelBuilder.Entity("ServerProject.Models.Marks", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("RollNumber");

                    b.Property<int>("Status");

                    b.Property<int>("Type");

                    b.Property<int>("TypeMark");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("RollNumber");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("ServerProject.Models.RollNumberStudents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alphabet")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("Number");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("RollNumberStudents");
                });

            modelBuilder.Entity("ServerProject.Models.StudentGrade", b =>
                {
                    b.Property<string>("RollNumber");

                    b.Property<int>("GradeId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("JoinAt");

                    b.Property<DateTime>("LeftAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("RollNumber", "GradeId");

                    b.HasIndex("GradeId");

                    b.ToTable("StudentGrade");
                });

            modelBuilder.Entity("ServerProject.Models.Students", b =>
                {
                    b.Property<string>("RollNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("RollNumber");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ServerProject.Models.GradeCourse", b =>
                {
                    b.HasOne("ServerProject.Models.Courses", "Courses")
                        .WithMany("GradeCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServerProject.Models.Grades", "Grades")
                        .WithMany("GradeCourses")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServerProject.Models.Informations", b =>
                {
                    b.HasOne("ServerProject.Models.Accounts", "Accounts")
                        .WithOne("Informations")
                        .HasForeignKey("ServerProject.Models.Informations", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServerProject.Models.Marks", b =>
                {
                    b.HasOne("ServerProject.Models.Courses", "Courses")
                        .WithMany("Markses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServerProject.Models.Students", "Students")
                        .WithMany("Markses")
                        .HasForeignKey("RollNumber");
                });

            modelBuilder.Entity("ServerProject.Models.StudentGrade", b =>
                {
                    b.HasOne("ServerProject.Models.Grades", "Grades")
                        .WithMany("StudentGrades")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ServerProject.Models.Students", "Students")
                        .WithMany("StudentGrades")
                        .HasForeignKey("RollNumber")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ServerProject.Models.Students", b =>
                {
                    b.HasOne("ServerProject.Models.Accounts", "Accounts")
                        .WithOne("Students")
                        .HasForeignKey("ServerProject.Models.Students", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
