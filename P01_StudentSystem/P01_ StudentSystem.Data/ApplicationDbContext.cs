using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.P01__StudentSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.P01__StudentSystem.Data
{
    internal class ApplicationDbContext :DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Data Source=PASAA;" +
                "Initial Catalog=EF_Task2;" +
                "Integrated Security=True;" +
                "TrustServerCertificate=True"
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
             .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                .IsUnicode(true)
                .HasMaxLength(100);

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                .IsUnicode(false)
                .HasMaxLength(10)
                .IsRequired(false);

            modelBuilder.Entity<Student>()
                .Property(s => s.Birthday)
                .IsRequired(false);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsUnicode(true)
                .HasMaxLength(80);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .IsUnicode(true)
                .IsRequired(false);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Course)
                .WithMany(c => c.Homeworks)
                .HasForeignKey(h => h.CourseId);

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Name)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Url)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Homework>()
                .Property(h => h.Content)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<Resource>()
                .Property(r => r.ResourceType)
                .HasConversion<int>();

            modelBuilder.Entity<Homework>()
                .Property(h => h.ContentType)
                .HasConversion<int>();
        }
    }
}
