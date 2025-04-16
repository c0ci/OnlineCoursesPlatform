using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Models;
namespace OnlineCoursesPlatform.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // SEED данни – примерни курсове
        builder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Title = "C# Fundamentals",
                Category = "Programming",
                LecturerId = 1
            },
            new Course
            {
                Id = 2,
                Title = "Web Development Basics",
                Category = "Web",
                LecturerId = 1
            }
        );

        // SEED данни – примерни лекции
        builder.Entity<Lecture>().HasData(
            new Lecture
            {
                Id = 1,
                Title = "Variables and Data Types",
                Description = "Basics of C#",
                CourseId = 1
            },
            new Lecture
            {
                Id = 2,
                Title = "HTML & CSS",
                Description = "Structure and Style",
                CourseId = 2
            }
        );
    }


}
