using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartUniversity.Domain.Entities;

namespace SmartUniversity.Infrastructure.Persistence { 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<AttendanceSession> AttendanceSessions { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizSubmission> QuizSubmissions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<CourseGrade> CourseGrades { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<User>()
           .HasIndex(u => u.NationalId)
           .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.NationalId)
                .HasMaxLength(14)
                .IsRequired();


            modelBuilder.Entity<Faculty>()
                .Property(f => f.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Faculty>()
                .HasIndex(f => f.Name)
                .IsUnique();
        }
    }
}