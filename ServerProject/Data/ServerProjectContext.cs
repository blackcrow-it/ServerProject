using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject.Models
{
    public class ServerProjectContext : DbContext
    {
        public ServerProjectContext(DbContextOptions<ServerProjectContext> options)
            : base(options)
        {
        }

       

        public DbSet<ServerProject.Models.Accounts> Accounts { get; set; }
        public DbSet<ServerProject.Models.RollNumberStudents> RollNumberStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary Key AccountId (Informations)
            modelBuilder.Entity<Informations>()
                .HasKey(i => new { i.AccountId });
            //Foreign Key AccountId (Informations)
            modelBuilder.Entity<Informations>()
                .HasOne(i => i.Accounts)
                .WithOne(a => a.Informations)
                .HasForeignKey<Informations>(i => i.AccountId);

            //Primary Key RollNumber (Students)
            modelBuilder.Entity<Students>()
                .HasKey(s => new { s.RollNumber });
            //Foreign Key AccountId (Students)
            modelBuilder.Entity<Students>()
                .HasOne(s => s.Accounts)
                .WithOne(a => a.Students)
                .HasForeignKey<Students>(s => s.AccountId);

            //Primary Key RollNumber and GradeId (StudentGrade)
            modelBuilder.Entity<StudentGrade>()
                .HasKey(s => new { s.RollNumber, s.GradeId });
            //Foreign Key RollNumber (StudentGrade)
            modelBuilder.Entity<StudentGrade>()
                .HasOne(sg => sg.Students)
                .WithMany(s => s.StudentGrades)
                .HasForeignKey(sg => sg.RollNumber);
            //Foreign Key GradeId (StudentGrade)
            modelBuilder.Entity<StudentGrade>()
                .HasOne(sg => sg.Grades)
                .WithMany(g => g.StudentGrades)
                .HasForeignKey(sg => sg.GradeId);

            //Foreign Key RollNumber (Marks)
            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Students)
                .WithMany(s => s.Markses)
                .HasForeignKey(m => m.RollNumber);
            //Foreign Key TypeMark (Marks)
            //modelBuilder.Entity<Marks>()
            //    .HasOne(m => m.Types)
            //    .WithMany(t => t.Markses)
            //    .HasForeignKey(m => m.TypeMark);
            //Foreign Key CourseId (Marks)
            modelBuilder.Entity<Marks>()
                .HasOne(m => m.Courses)
                .WithMany(t => t.Markses)
                .HasForeignKey(m => m.CourseId);

            //Primary Key GradeId and CoursesId (GradeCourse)
            modelBuilder.Entity<GradeCourse>()
                .HasKey(gc => new { gc.GradeId, gc.CourseId });
            //Foreign Key GradeId (GradeCourse)
            modelBuilder.Entity<GradeCourse>()
                .HasOne(gc => gc.Grades)
                .WithMany(g => g.GradeCourses)
                .HasForeignKey(gc => gc.GradeId);
            //Foreign Key CoursesId (GradeCourse)
            modelBuilder.Entity<GradeCourse>()
                .HasOne(gc => gc.Courses)
                .WithMany(g => g.GradeCourses)
                .HasForeignKey(gc => gc.CourseId);

            //Not Mapped
            modelBuilder.Entity<Accounts>()
                .Ignore(a => a.ConfirmPassword);
        }

        public DbSet<ServerProject.Models.Informations> Informations { get; set; }

        public DbSet<ServerProject.Models.Students> Students { get; set; }

        public DbSet<ServerProject.Models.StudentGrade> StudentGrade { get; set; }

        public DbSet<ServerProject.Models.Grades> Grades { get; set; }

        public DbSet<ServerProject.Models.GradeCourse> GradeCourse { get; set; }

        public DbSet<ServerProject.Models.Courses> Courses { get; set; }

        public DbSet<ServerProject.Models.Marks> Marks { get; set; }
        public DbSet<ServerProject.Models.Credential> Credentials { get; set; }

        //public DbSet<ServerProject.Models.Types> Types { get; set; }


    }
}