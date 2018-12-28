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
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
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

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UserName");

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

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Courses");
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

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("ServerProject.Models.Informations", b =>
                {
                    b.Property<int>("AccountId");

                    b.Property<string>("Address");

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Phone");

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

                    b.Property<int>("TypeMark");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("RollNumber");

                    b.HasIndex("TypeMark");

                    b.ToTable("Marks");
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

            modelBuilder.Entity("ServerProject.Models.Types", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<float>("MaxValue");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Types");
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

                    b.HasOne("ServerProject.Models.Types", "Types")
                        .WithMany("Markses")
                        .HasForeignKey("TypeMark")
                        .OnDelete(DeleteBehavior.Cascade);
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
