using Xunit;
using OnlineCoursesPlatform.Controllers;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EnrollmentsControllerTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EnrollTest_" + System.Guid.NewGuid())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Enroll_AddsEnrollmentForStudent()
    {
        // Arrange
        var db = GetDbContext();

        var course = new Course { Id = 1, Title = "Test Course" };
        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var controller = new EnrollmentsController(db);

        // Създаваме fake user ID
        var userId = "student-123";
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = controller.Enroll(1);

        // Assert
        var enrollment = db.Enrollments.FirstOrDefault(e => e.StudentId == userId && e.CourseId == 1);
        Assert.NotNull(enrollment);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirect.ActionName);
    }
}
