using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Models;
namespace OnlineCoursesPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
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
                new Course { Id = 6, Title = "JavaScript Основи", Category = "Уеб", LecturerId = 1 },
                new Course { Id = 7, Title = "Бази от Данни", Category = "БД", LecturerId = 1 },
                new Course { Id = 8, Title = "ASP.NET MVC", Category = "Програмиране", LecturerId = 1 }
        );

        // SEED данни – примерни лекции
        builder.Entity<Lecture>().HasData(
                // JavaScript Основи
                new Lecture { Id = 3, Title = "Променливи и типове", Description = "Основни типове в JS", CourseId = 6 },
                new Lecture { Id = 4, Title = "Функции", Description = "Как работят функциите", CourseId = 6 },
                // Бази от Данни
                new Lecture { Id = 5, Title = "SQL Въведение", Description = "Какво е SQL и как се пише", CourseId = 7 },
                new Lecture { Id = 6, Title = "SELECT заявки", Description = "Извличане на данни от таблици", CourseId = 7 },
                // ASP.NET MVC
                new Lecture { Id = 7, Title = "MVC структура", Description = "Model, View, Controller", CourseId = 8 },
                new Lecture { Id = 8, Title = "Routing и действия", Description = "Как работи маршрутизацията", CourseId = 8 }
        );
    }


}
